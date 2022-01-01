using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Events;
using ZiggyCreatures.Caching.Fusion.Internals;

namespace ProsperousUniverse.API;

public sealed class CacheTracker
{
    private readonly IFusionCache _cache;
    private HashSet<string> _entries = new();

    public CacheTracker(IFusionCache cache)
    {
        _cache = cache;
        _cache.Events.Set += OnSet;
        _cache.Events.Remove += OnRemove;
    }

    private void OnRemove(object? sender, FusionCacheEntryEventArgs e)
    {
        _entries.Remove(e.Key);
    }

    private void OnSet(object? sender, FusionCacheEntryEventArgs e)
    {
        _entries.Add(e.Key);
    }

    public IEnumerable<string> Entries => _entries;
}
