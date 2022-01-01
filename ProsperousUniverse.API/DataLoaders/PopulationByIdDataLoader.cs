using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class PopulationByIdDataLoader : CacheDataLoader<string, PopulationDTO>
{
    private readonly IServerInterface _serverInterface;

    public PopulationByIdDataLoader(IServerInterface serverInterface)
    {
        _serverInterface = serverInterface;
    }
    
    protected override async Task<PopulationDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => PopulationDTO.Parse(await _serverInterface.GetData(new[] {"populations", key}));
}
