using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class UserByIdDataLoader : CacheDataLoader<string, UserDTO>
{
    private readonly DataCache _dataCache;

    public UserByIdDataLoader(
        DataCache dataCache,
        DataLoaderOptions? options = null) : base(options)
    {
        _dataCache = dataCache;
    }

    protected override async Task<UserDTO> LoadSingleAsync(string key, CancellationToken cancellationToken)
        => UserDTO.Parse(await _dataCache.GetData("users/" + key));
}
