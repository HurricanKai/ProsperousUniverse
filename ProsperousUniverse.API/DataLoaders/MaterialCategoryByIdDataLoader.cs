using ProsperousUniverse.API.DTOs;

namespace ProsperousUniverse.API.DataLoaders;

public sealed class MaterialCategoryByIdDataLoader : BatchDataLoader<string, MaterialCategoryDTO>
{
    private readonly MaterialCategories _materialCategories;
    public MaterialCategoryByIdDataLoader(IBatchScheduler batchScheduler, MaterialCategories materialCategories, DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _materialCategories = materialCategories;
    }

    protected override async Task<IReadOnlyDictionary<string, MaterialCategoryDTO>> LoadBatchAsync(
        IReadOnlyList<string> keys,
        CancellationToken cancellationToken)
        => (await _materialCategories.GetCategories())
            .Where(x => keys.Contains(x.Id))
            .ToDictionary(x => x.Id);
}
