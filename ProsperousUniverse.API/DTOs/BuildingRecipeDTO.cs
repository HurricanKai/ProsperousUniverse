using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("BuildingRecipe")]
[Authorize(Policy = "read:pu_building_recipe")]
public sealed class BuildingRecipeDTO
{
    [GraphQLIgnore]
    public string ReactorId { get; set; }

    [GraphQLName("reactor"), GraphQLNonNullType]
    public Task<ReactorDTO> GetReactorAsync([Service] ReactorByIdDataLoader reactorByIdDataLoader)
        => reactorByIdDataLoader.LoadAsync(ReactorId);

    [GraphQLNonNullType]
    public MaterialQuantityDTO[] BuildingCosts { get; set; }

    [GraphQLName("combinedPrice")]
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

    public static BuildingRecipeDTO Parse(JsonElement jsonElement)
        => new()
        {
            ReactorId = jsonElement.GetProperty("reactorId").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("reactorId"),
            BuildingCosts = jsonElement.GetProperty("buildingCosts").EnumerateArray().Select(MaterialQuantityDTO.Parse).ToArray()
        };
}