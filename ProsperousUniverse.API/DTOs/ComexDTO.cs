using System.Text.Json;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("Commex"), Node]
public sealed class ComexDTO : INode
{
    [GraphQLNonNullType, GraphQLType(typeof(IdType))]
    public string Id { get; set; }
    
    [GraphQLNonNullType]
    public string Name { get; set; }
    
    [GraphQLNonNullType]
    public string Code { get; set; }
    
    [GraphQLIgnore]
    public (string Type, string Id) OperatorEntity { get; set; }

    [GraphQLName("operator"), GraphQLNonNullType]
    public Task<IWorldEntityDTO> GetOperatorAsync([Service] WorldEntityResolver worldEntityResolver)
        => worldEntityResolver.GetWorldEntity(OperatorEntity.Type, OperatorEntity.Id);

    [GraphQLNonNullType]
    public CurrencyDTO Currency { get; set; }

    [GraphQLNonNullType]
    public AddressDTO Address { get; set; }

    [GraphQLName("brokerCategoryOf"), GraphQLNonNullType]
    public Task<BrokerCategoryDTO> GetBrokerCategoryOf(
        string materialCategoryId,
        [Service]
        BrokerCategoryByMaterialCategoryIdAndComexIdDataLoader brokerCategoryByMaterialCategoryIdAndComexIdDataLoader)
        => brokerCategoryByMaterialCategoryIdAndComexIdDataLoader.LoadAsync((materialCategoryId, Id));

    [GraphQLName("brokerCategories"), GraphQLNonNullType]
    public async IAsyncEnumerable<BrokerCategoryDTO> GetBrokerCategories([Service] MaterialCategories materialCategories,
        [Service] BrokerCategoryByMaterialCategoryIdAndComexIdDataLoader brokerCategoryByMaterialCategoryIdAndComexIdDataLoader)
    {
        foreach (var materialCategory in await materialCategories.GetCategories())
        {
            yield return await GetBrokerCategoryOf(materialCategory.Id, brokerCategoryByMaterialCategoryIdAndComexIdDataLoader);
        }
    }

    public static ComexDTO Parse(JsonElement jsonElement)
    {
        var @operator = jsonElement.GetProperty("operator");
        return new()
        {
            Id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id"),
            Name = jsonElement.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("name"),
            Code = jsonElement.GetProperty("code").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("code"),
            OperatorEntity = (
                @operator.GetProperty("_type").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("operator._type"),
                @operator.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("operator.id")
                ),
            Currency = CurrencyDTO.Parse(jsonElement.GetProperty("currency")),
            Address = AddressDTO.Parse(jsonElement.GetProperty("address"))
        };
    }

    [GraphQLName("brokerCategory")]
    public Task<BrokerCategoryDTO> GetBrokerCategoryAsync(
        string materialCategoryId,
        [Service] BrokerCategoryByMaterialCategoryIdAndComexIdDataLoader brokerCategoryByMaterialCategoryIdAndComexIdDataLoader)
        => brokerCategoryByMaterialCategoryIdAndComexIdDataLoader.LoadAsync((materialCategoryId, Id));
    

    [GraphQLName("allBrokerCategories")]
    public async IAsyncEnumerable<BrokerCategoryDTO> GetAllBrokersAsync([Service] MaterialCategories materialCategories,
        [Service] BrokerCategoryByMaterialCategoryIdAndComexIdDataLoader brokerCategoryByMaterialCategoryIdAndComexIdDataLoader)
    {
        foreach (var category in await materialCategories.GetCategories())
        {
            yield return await GetBrokerCategoryAsync(category.Id,
                brokerCategoryByMaterialCategoryIdAndComexIdDataLoader);
        }
    }

    [NodeResolver]
    public static Task<ComexDTO> Get(string id, [Service] ComexByIdDataLoader comexByIdDataLoader)
        => comexByIdDataLoader.LoadAsync(id);
}
