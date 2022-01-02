using System.Diagnostics;
using System.Text.Json;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Events;

namespace ProsperousUniverse.API;

public sealed class DataCache
{
    private readonly IFusionCache _cache;
    private readonly IServerInterface _serverInterface;
    private readonly HashSet<string> _subscriptions = new();
    private readonly ReaderWriterLock _subscriptionLock = new();

    public DataCache(IFusionCache cache, IServerInterface serverInterface)
    {
        _cache = cache;
        _serverInterface = serverInterface;
    }

    private const string CACHE_PREFIX = "PU_DATA";

    public ValueTask<JsonElement> GetData(string path)
    {
        return _cache.GetOrSetAsync(CACHE_PREFIX + path, async x => (await _serverInterface.GetData(path.Trim('/').Split('/'))).GetProperty("body"));
    }
}
