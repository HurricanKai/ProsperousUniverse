using System.Diagnostics;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class ReactorByIdDataLoader : CacheDataLoader<string, ReactorDTO>
{
    private readonly IServerInterface _serverInterface;

    public ReactorByIdDataLoader(IServerInterface serverInterface)
    {
        _serverInterface = serverInterface;
    }
    
    protected override Task<ReactorDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => _serverInterface.DoAction(
        x => _serverInterface.SendMessage(new BaseMessage(ActionNames.WorldFindReactorData, new { actionId = x, query = key },
            "world-data")), x =>
        {
            Debug.Assert(x!.MessageType == ActionNames.WorldReactorData);
            return ReactorDTO.Parse(x.Payload);
        });
}
