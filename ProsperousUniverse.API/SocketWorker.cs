using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Polly;
using Polly.Bulkhead;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;
using SocketIOClient;
using SocketIOClient.Transport;

namespace ProsperousUniverse.API;

public class SocketWorker
    : IHostedService, IServerInterface, IDisposable
{
    private readonly IServiceProvider _provider;
    private readonly ILogger _logger;
    private readonly SocketIO _socketIo;
    private static readonly JsonElement Empty = JsonDocument.Parse("{}").RootElement;
    private AuthSession? _session;
    private HttpClientHandler? _handler;
    private PresentUser[] _users = Array.Empty<PresentUser>();
    private ConcurrentDictionary<string, TaskCompletionSource<BaseEvent?>> _actionCallbacks = new();
    private IDataDataHandler? _dataDataHandler;

    public PresentUser[] PresentUsers => _users;

    private TaskCompletionSource _postAuthTcs = new();
    private readonly Meter _meter;
    private readonly Counter<long> _messageCount;
    private readonly Counter<long> _eventCount;
    private readonly SemaphoreSlim _actionSemaphore;
    private const int MaxConcurrentActions = 10; // We don't want to overload the server :)

    public Task WaitForPostAuth()
    {
        return _postAuthTcs.Task;
    }

    public SocketState SocketState { get; private set; } = SocketState.Offline;

    public SocketWorker(ILoggerFactory loggerFactory, IServiceProvider provider)
    {
        _provider = provider;
        _logger = loggerFactory.CreateLogger<SocketWorker>();
        _socketIo = new SocketIO(loggerFactory, "https://apex.prosperousuniverse.com/");
        _socketIo.Options.Transport = TransportProtocol.Polling;
        _socketIo.Options.EIO = 3;
        _meter = new Meter("PU.API.SocketWorker");
        _messageCount = _meter.CreateCounter<long>("message-count");
        _eventCount = _meter.CreateCounter<long>("event-count");
        _actionSemaphore = new SemaphoreSlim(MaxConcurrentActions, MaxConcurrentActions);
    }

    public void SetDataDataHandler(IDataDataHandler dataDataHandler)
    {
        if (_dataDataHandler is not null) throw new InvalidOperationException();
        _dataDataHandler = dataDataHandler;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _provider.CreateAsyncScope();
        var authService = scope.ServiceProvider.GetRequiredService<AuthService>();
        _session = await authService.Authenticate();

        _socketIo.ClientWebSocketProvider = () => new DefaultClientWebSocket(_session.Token);
        _handler = new HttpClientHandler();
        _handler.UseCookies = true;
        _handler.CookieContainer.Add(new Cookie("pu-sid", _session.Token, "/", ".prosperousuniverse.com"));
        _handler.CookieContainer.Add(new Cookie("_sl_ref", "https%3A%2F%2Fprosperousuniverse.com%2F", "/",
            ".prosperousuniverse.com"));
        _socketIo.HttpClient = new HttpClient(_handler);
        _socketIo.OnPing += (sender, args) => _logger.LogInformation("Ping");
        _socketIo.OnFallbackAny((name, response)
            => _logger.LogInformation("Unhandled {Name}: {Response}", name, response.ToString()));
        _socketIo.On("event", HandleEventsRaw);
        SocketState = SocketState.Connecting;
        await _socketIo.ConnectAsync();

        await WaitForPostAuth();
    }

    private void HandleEventsRaw(SocketIOResponse obj)
    {
        for (int i = 0; i < obj.Count; i++)
        {
            var @event = obj.GetValue(i).Deserialize<BaseEvent>();
            if (@event is null) continue;
            HandleEvent(@event);
        }
    }

    private void HandleEvent(BaseEvent @event)
    {
        _eventCount.Add(1);
        switch (@event.MessageType)
        {
            case ActionNames.PresenceList:
                var presenceList = @event.Payload.Deserialize<PresenceList>();
                _users = presenceList?.Users ?? _users;
                _logger.LogDebug("Received Presence List with {UserCount} Users", presenceList?.Users?.Length ?? 0);
                break;
            case ActionNames.ServerConnectionOpened:
                Debug.Assert(SocketState == SocketState.Connecting);
                SocketState = SocketState.Authenticating;
                _ = SendMessage(new BaseMessage(ActionNames.AuthAuthenticate, Empty));
                break;
            case ActionNames.AuthAuthenticated:
                Debug.Assert(SocketState == SocketState.Authenticating);
                SocketState = SocketState.PostAuth;
                _postAuthTcs.SetResult();
                var username = @event.Payload.GetProperty("username").GetString();
                _logger.LogInformation("Authenticated with transport as {Username}", username);
                break;
            case ActionNames.AuthUnauthenticated:
                Debug.Assert(SocketState == SocketState.Authenticating);
                _logger.LogCritical("Manual authentication necessary");
                throw new Exception();
            case ActionNames.ActionCompleted:
                Debug.Assert(SocketState == SocketState.PostAuth);
                var actionId = @event.Payload.GetProperty("actionId").GetString();
                var status = @event.Payload.GetProperty("status").GetInt32();
                if (status != 0) _logger.LogError("Status was not 0! was: {Status}", status);
                var event2 = @event.Payload.GetProperty("message").Deserialize<BaseEvent>();
                if (actionId is null) break;
                if (_actionCallbacks.Remove(actionId, out var handler))
                {
                    if (status != 0)
                    {
                        _logger.LogError("Status was not 0! was: {Status}", status);
                        handler.SetException(new InvalidOperationException($"Server returned non-success: {status}"));
                    }
                    else
                    {
                        handler.SetResult(event2);
                    }
                }
                else
                {
                    _logger.LogError("Unknown ActionID {ActionId}", actionId);
                }
                break;
            case ActionNames.DataData:
                Debug.Assert(SocketState == SocketState.PostAuth);
                _dataDataHandler?.HandleDataData(@event);   
                break;
            
            case ActionNames.DataDataRemoved:
                Debug.Assert(SocketState == SocketState.PostAuth);
                _dataDataHandler?.HandleDataRemoved(@event);   
                break;

            default:
                _logger.LogWarning("Unhandled event {MessageType} {Payload}", @event.MessageType,
                    @event.Payload.ToString());
                break;
        }
    }

    public async Task SendMessage(BaseMessage message)
    {
        _messageCount.Add(1);
        await _socketIo.EmitAsync("message", message);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _socketIo.DisconnectAsync();
    }

    private static readonly IAsyncPolicy<BaseEvent?> DoActionPolicy = Policy
        .Handle<TaskCanceledException>()
        .Or<OperationCanceledException>()
        .WaitAndRetryAsync(15, x => TimeSpan.FromSeconds(x))
        .AsAsyncPolicy<BaseEvent?>();

    public Task<BaseEvent?> DoAction(Func<string, Task> action, CancellationToken cancellationToken, int timeout)
    {   
        return DoActionPolicy.ExecuteAsync(async c =>
        {
            // this is a super critical point and needs to sustain large peaks of requests at a time, especially after startup
            var tcs = new TaskCompletionSource<BaseEvent?>();
            var id = Guid.NewGuid().ToString();
            _actionCallbacks[id] = tcs;
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(c);
            await _actionSemaphore.WaitAsync(cts.Token);
            if (timeout != Timeout.Infinite)
                cts.CancelAfter(timeout);
            var v = cts.Token.Register(() => tcs.SetCanceled(), false);
            try
            {
                await action(id);
                return await tcs.Task;
            }
            finally
            {
                _actionSemaphore.Release();
                await v.DisposeAsync(); // merge await using & try-finally into one construct
                _actionCallbacks.TryRemove(id, out _); // prevent dictionary slowly filling up with timed-out ids
            }
        }, cancellationToken);
    }

    public async Task<JsonElement> GetData(string[] path, DataFilters? filters, CancellationToken cancellationToken, int timeout)
    {
        var e = await DoAction(
            x => SendMessage(new BaseMessage(ActionNames.DataGetData, new { actionId = x, path = path, filters = filters })),
            cancellationToken, timeout);
        Debug.Assert(e.MessageType == ActionNames.DataData);
        return e.Payload;
    }

    public void Dispose()
    {
        _actionSemaphore.Dispose();
        _socketIo.Dispose();
        _handler?.Dispose();
        _meter.Dispose();
    }
}
