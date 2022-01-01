using System.Diagnostics;
using System.Text.Json;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Events;

namespace ProsperousUniverse.API;

public sealed class DataCache : IDataDataHandler
{
    private readonly IFusionCache _cache;
    private readonly IServerInterface _serverInterface;
    private readonly HashSet<string> _subscriptions = new();
    private readonly ReaderWriterLock _subscriptionLock = new();

    public DataCache(IFusionCache cache, IServerInterface serverInterface)
    {
        _cache = cache;
        _serverInterface = serverInterface;
        _cache.Events.Set += OnSet;
        _cache.Events.Remove += OnRemove;
        serverInterface.SetDataDataHandler(this);
    }

    private const string CACHE_PREFIX = "PU_DATA";

    private void OnRemove(object? sender, FusionCacheEntryEventArgs e)
    {
        if (e.Key.StartsWith(CACHE_PREFIX))
        {
            var path = e.Key[7..];
            _subscriptionLock.AcquireReaderLock(TimeoutMillis);
            try
            {
                if (!_subscriptions.Contains(path)) return;
            }
            finally
            {
                _subscriptionLock.ReleaseReaderLock();
            }

            _subscriptionLock.AcquireWriterLock(TimeoutMillis);
            try
            {
                _subscriptions.Remove(path);
            }
            finally
            {
                _subscriptionLock.ReleaseWriterLock();
            }

            SendSubscriptions();
        }
    }

    const int TimeoutMillis = (int)(1.0 * 1000);
    private void OnSet(object? sender, FusionCacheEntryEventArgs e)
    {
        if (e.Key.StartsWith(CACHE_PREFIX))
        {
            var path = e.Key[7..];
            _subscriptionLock.AcquireReaderLock(TimeoutMillis);
            try
            {
                if (_subscriptions.Contains(path)) return;
            }
            finally
            {
                _subscriptionLock.ReleaseReaderLock();
            }

            _subscriptionLock.AcquireWriterLock(TimeoutMillis);
            try
            {
                _subscriptions.Add(path);
            }
            finally
            {
                _subscriptionLock.ReleaseWriterLock();
            }

            SendSubscriptions();
        }
    }

    private void SendSubscriptions()
    {
        _subscriptionLock.AcquireReaderLock(TimeoutMillis);
        try
        {
            _serverInterface.SendMessage(new BaseMessage(ActionNames.SubscriptionsUpdate, new { topics = _subscriptions })).Wait();
        }
        finally
        {
            _subscriptionLock.ReleaseReaderLock();
        }
    }

    public ValueTask<JsonElement> GetData(string path)
    {
        return _cache.GetOrSetAsync(CACHE_PREFIX + path, async x => (await _serverInterface.GetData(path.Trim('/').Split('/'))).GetProperty("body"));
    }

    public async Task HandleDataData(BaseEvent @event)
    {
        Debug.Assert(@event.MessageType == ActionNames.DataData);
        var path = "/" + string.Join('/', @event.Payload.GetProperty("path").EnumerateArray()) + "/";
        await _cache.SetAsync(path,
            @event.Payload);
        // TODO: Somehow notify GraphQL here
    }

    public async Task HandleDataRemoved(BaseEvent @event)
    {
        Debug.Assert(@event.MessageType == ActionNames.DataDataRemoved);
        var path = "/" + string.Join('/', @event.Payload.GetProperty("path").EnumerateArray()) + "/";
        await _cache.RemoveAsync(path);
        // TODO: Somehow notify GraphQL here
    }
}
