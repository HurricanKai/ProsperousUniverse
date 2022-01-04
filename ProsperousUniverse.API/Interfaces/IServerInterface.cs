using System.Text.Json;
using ProsperousUniverse.API.JsonModels;

namespace ProsperousUniverse.API.Interfaces;

public interface IServerInterface
{
    SocketState SocketState { get; }
    PresentUser[] PresentUsers { get; }
    Task WaitForPostAuth();
    Task SendMessage(BaseMessage message);
    private const int DefaultTimeout = 5000;
    Task<BaseEvent?> DoAction(Func<string, Task> action, CancellationToken cancellationToken = default, int timeout = DefaultTimeout);
    Task<JsonElement> GetData(string[] path, DataFilters? filters = null, CancellationToken cancellationToken = default, int timeout = DefaultTimeout);
    void SetDataDataHandler(IDataDataHandler handler);
}