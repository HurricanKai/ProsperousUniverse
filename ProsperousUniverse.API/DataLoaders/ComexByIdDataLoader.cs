using ProsperousUniverse.API.DTOs;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class ComexByIdDataLoader : BatchDataLoader<string, ComexDTO>
{
    private readonly CommodityExchangeList _commodityExchangeList;

    public ComexByIdDataLoader(
        CommodityExchangeList commodityExchangeList,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _commodityExchangeList = commodityExchangeList;
    }

    protected override async Task<IReadOnlyDictionary<string, ComexDTO>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
    {
        return (await _commodityExchangeList.GetCommodityExchanges())
            .Where(x => keys.Contains(x.Id))
            .ToDictionary(x => x.Id);
    }
}
