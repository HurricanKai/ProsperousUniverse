using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Events;
using ZiggyCreatures.Caching.Fusion.Internals;

namespace ProsperousUniverse.API;

public sealed class CacheTracker
{
    private readonly IFusionCache _cache;
    private HashSet<string> _memEntries = new();
    private HashSet<string> _distEntries = new();

    public CacheTracker(IFusionCache cache)
    {
        _cache = cache;
        _cache.Events.Memory.Set += OnSetMemory;
        _cache.Events.Memory.Remove += OnRemoveMemory;
        _cache.Events.Memory.Set += OnSetDist;
        _cache.Events.Memory.Remove += OnRemoveDist;
    }

    private void OnRemoveMemory(object? sender, FusionCacheEntryEventArgs e)
    {
        _memEntries.Remove(e.Key);
    }

    private void OnSetMemory(object? sender, FusionCacheEntryEventArgs e)
    {
        _memEntries.Add(e.Key);
    }
    
    private void OnRemoveDist(object? sender, FusionCacheEntryEventArgs e)
    {
        _distEntries.Remove(e.Key);
    }

    private void OnSetDist(object? sender, FusionCacheEntryEventArgs e)
    {
        _distEntries.Add(e.Key);
    }

    public IEnumerable<string> MemoryEntries => _memEntries;
    public IEnumerable<string> DistributedEntries => _distEntries;
}
