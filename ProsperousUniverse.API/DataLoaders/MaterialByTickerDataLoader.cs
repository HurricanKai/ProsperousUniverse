using System.Diagnostics;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;
using ZiggyCreatures.Caching.Fusion;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class MaterialByTickerDataLoader : CacheDataLoader<string, MaterialDTO>
{
    private readonly IServerInterface _serverInterface;
    private readonly IFusionCache _cache;

    public MaterialByTickerDataLoader(IServerInterface serverInterface, IFusionCache cache) : base(new DataLoaderOptions())
    {
        _serverInterface = serverInterface;
        _cache = cache;
    }

    protected override async Task<MaterialDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => await _cache.GetOrSetAsync("PU_MAT_TIK" + key, async c =>
        {
            var m = await _serverInterface.DoAction(
                    x => _serverInterface.SendMessage(new BaseMessage(ActionNames.WorldFindMaterialData,
                        new { query = key, actionId = x }, "world-data")), c);
            Debug.Assert(m.MessageType == ActionNames.WorldMaterialData);
            return MaterialDTO.Parse(m.Payload);
        }, token: cancellationToken);
}
