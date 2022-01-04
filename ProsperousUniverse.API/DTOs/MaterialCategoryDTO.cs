using System.Diagnostics;
using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[Node, GraphQLName("MaterialCategory")]
[Authorize(Policy = "read:pu_material_category")]
public sealed class MaterialCategoryDTO : INode
{
    [GraphQLNonNullType, GraphQLType(typeof(IdType))]
    public string Id { get; set; }

    [GraphQLNonNullType]
    public string Name { get; set; }
    
    [GraphQLIgnore]
    public string[] MaterialIds { get; set; }

    [GraphQLNonNullType, GraphQLName("materials")]
    // [UsePaging]
    public async IAsyncEnumerable<MaterialDTO> GetMaterialsAsync(
        [Service] MaterialByIdDataLoader materialByIdDataLoader)
    {
        foreach (var id in MaterialIds)
            yield return await materialByIdDataLoader.LoadAsync(id);
    }

    [GraphQLName("brokerCategoryAt"), GraphQLNonNullType]
    public Task<BrokerCategoryDTO> GetBrokerCategoryAt(
        string comexId,
        [Service]
        BrokerCategoryByMaterialCategoryIdAndComexIdDataLoader brokerCategoryByMaterialCategoryIdAndComexIdDataLoader)
        => brokerCategoryByMaterialCategoryIdAndComexIdDataLoader.LoadAsync((Id, comexId));

    [GraphQLName("brokerCategories"), GraphQLNonNullType]
    public async IAsyncEnumerable<BrokerCategoryDTO> GetBrokerCategories([Service] CommodityExchangeList commodityExchangeList,
        [Service] BrokerCategoryByMaterialCategoryIdAndComexIdDataLoader brokerCategoryByMaterialCategoryIdAndComexIdDataLoader)
    {
        foreach (var comex in await commodityExchangeList.GetCommodityExchanges())
        {
            yield return await GetBrokerCategoryAt(comex.Id, brokerCategoryByMaterialCategoryIdAndComexIdDataLoader);
        }
    }

    [NodeResolver]
    public static Task<MaterialCategoryDTO> Get(
        string id,
        [Service] MaterialCategoryByIdDataLoader materialCategoryByIdDataLoader)
        => materialCategoryByIdDataLoader.LoadAsync(id);

    public static MaterialCategoryDTO Parse(JsonElement jsonElement)
    {
        Debug.Assert(jsonElement.GetProperty("children").GetArrayLength() == 0);
        return new()
        {
            Id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id"),
            Name = jsonElement.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("name"),
            MaterialIds = jsonElement.GetProperty("materials").EnumerateArray().Select(x
                => x.GetProperty("id").GetString() ??
                   ParsingUtils.ThrowRequiredPropertyMissing("materials[?].ticker")).ToArray()
        };
    }
}
