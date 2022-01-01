using ProsperousUniverse.API.DTOs;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class PlanetByIdDataLoader : CacheDataLoader<string, PlanetDTO>
{
    private readonly DataCache _dataCache;

    public PlanetByIdDataLoader(
        DataCache dataCache,
        DataLoaderOptions? options = null) : base(options)
    {
        _dataCache = dataCache;
    }

    protected override async Task<PlanetDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => PlanetDTO.Parse(await _dataCache.GetData("planets/" + key));
}
