using System.Text.Json;
using ProsperousUniverse.API.JsonModels;

namespace ProsperousUniverse.API.Interfaces;

public interface IServerInterface
{
    SocketState SocketState { get; }
    PresentUser[] PresentUsers { get; }
    Task WaitForPostAuth();
    Task SendMessage(BaseMessage message);
    Task<T> DoAction<T>(Func<string, Task> action, Func<BaseEvent?, T> callback);
    Task<JsonElement> GetData(string[] path, DataFilters? filters = null);
    void SetDataDataHandler(IDataDataHandler handler);
}