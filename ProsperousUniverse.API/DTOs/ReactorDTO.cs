using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("Reactor"), Node]
[Authorize(Policy = "read:pu_reactor")]
public sealed class ReactorDTO : INode
{
    [GraphQLNonNullType, GraphQLType(typeof(IdType))]
    public string Id { get; set; }

    [GraphQLNonNullType]
    public string Name { get; set; }
    
    [GraphQLNonNullType]
    public string Ticker { get; set; }
    
    [GraphQLNonNullType]
    public string Expertise { get; set; }

    public int AreaCost { get; set; }
    
    [GraphQLNonNullType]
    public MaterialQuantityDTO[] BuildingCosts { get; set; }

    [GraphQLName("combinedBuildingPrice")]
    public async Task<double> GetCombinedPrice(
        string comex,
        [Service] MaterialByIdDataLoader materialByIdDataLoader,
        [Service] BrokerByTickerDataLoader brokerByTickerDataLoader
    )
    {
        double d = 0;
        foreach (var i in BuildingCosts)
            d += (await (await i.GetMaterialAsync(materialByIdDataLoader)).GetBrokerAtAsync(comex,
                brokerByTickerDataLoader))?.PriceAverage?.Amount ?? Double.NaN;
        return d;
    }
    
    [GraphQLNonNullType]
    public WorkforceCapacityDTO[] WorkforceCapacities {get; set; }
    
    [GraphQLNonNullType]
    public RecipeDTO[] Recipes { get; set; }

    public static ReactorDTO Parse(JsonElement jsonElement)
    {
        var id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id");
        return new()
        {
            Id = id,
            Name = jsonElement.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("name"),
            Ticker =
                jsonElement.GetProperty("ticker").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("ticker"),
            Expertise =
                jsonElement.GetProperty("expertise").GetString() ??
                ParsingUtils.ThrowRequiredPropertyMissing("expertise"),
            AreaCost = jsonElement.GetProperty("areaCost").GetInt32(),
            BuildingCosts =
                jsonElement.GetProperty("buildingCosts").EnumerateArray().Select(MaterialQuantityDTO.Parse).ToArray(),
            WorkforceCapacities =
                jsonElement.GetProperty("workforceCapacities").EnumerateArray().Select(WorkforceCapacityDTO.Parse)
                    .ToArray(),
            Recipes = jsonElement.GetProperty("recipes").EnumerateArray().Select(j => RecipeDTO.Parse(j, id)).ToArray()
        };
    }


    [NodeResolver]
    public static Task<ReactorDTO> Get(string id, [Service] ReactorByIdDataLoader reactorByIdDataLoader)
        => reactorByIdDataLoader.LoadAsync(id);
}