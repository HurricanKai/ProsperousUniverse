using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;
using ZiggyCreatures.Caching.Fusion;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class ReactorByIdDataLoader : CacheDataLoader<string, ReactorDTO>
{
    private readonly IServerInterface _serverInterface;
    private readonly IFusionCache _cache;

    public ReactorByIdDataLoader(IServerInterface serverInterface, IFusionCache cache)
    {
        _serverInterface = serverInterface;
        _cache = cache;
    }
    
    protected override async Task<ReactorDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => await _cache.GetOrSetAsync("PU_REACTOR" + key, c => _serverInterface.DoAction(
        x => _serverInterface.SendMessage(new BaseMessage(ActionNames.WorldFindReactorData, new { actionId = x, query = key },
            "world-data")), x =>
        {
            Debug.Assert(x!.MessageType == ActionNames.WorldReactorData);
            return ReactorDTO.Parse(x.Payload);
        }), token: cancellationToken);
}
