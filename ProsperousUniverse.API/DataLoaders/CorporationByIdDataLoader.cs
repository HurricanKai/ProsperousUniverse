using ProsperousUniverse.API.DTOs;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class CorporationByIdDataLoader : CacheDataLoader<string, CorporationDTO>
{
    private readonly DataCache _dataCache;

    public CorporationByIdDataLoader(
        DataCache dataCache,
        DataLoaderOptions? options = null) : base(options)
    {
        _dataCache = dataCache;
    }

    protected override async Task<CorporationDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => CorporationDTO.Parse(await _dataCache.GetData("corporations/" + key));
}
