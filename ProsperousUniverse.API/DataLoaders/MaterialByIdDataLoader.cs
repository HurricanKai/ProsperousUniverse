using System.Diagnostics;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class MaterialByIdDataLoader : CacheDataLoader<string, MaterialDTO>
{
    private readonly IServerInterface _serverInterface;

    public MaterialByIdDataLoader(IServerInterface serverInterface) : base(new DataLoaderOptions())
    {
        _serverInterface = serverInterface;
    }

    protected override Task<MaterialDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => _serverInterface.DoAction(
            x => _serverInterface.SendMessage(new BaseMessage(ActionNames.WorldFindMaterialData,
                new { query = key, actionId = x }, "world-data")), m =>
            {
                Debug.Assert(m.MessageType == ActionNames.WorldMaterialData);
                return MaterialDTO.Parse(m.Payload);
            });
}
