using ProsperousUniverse.API.JsonModels;

namespace ProsperousUniverse.API.Interfaces;

public interface IDataDataHandler
{
    Task HandleDataData(BaseEvent @event);
    Task HandleDataRemoved(BaseEvent @event);
}
