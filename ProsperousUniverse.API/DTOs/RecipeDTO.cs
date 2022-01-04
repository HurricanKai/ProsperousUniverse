using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("Recipe")]
[Authorize(Policy = "read:pu_recipe")]
public sealed class RecipeDTO
{
    [GraphQLNonNullType]
    public MaterialQuantityDTO[] Inputs { get; set; }
    
    [GraphQLNonNullType]
    public MaterialQuantityDTO[] Outputs { get; set; }
    
    public long Duration { get; set; }
    
    [GraphQLIgnore]
    public string ReactorId { get; set; }

    [GraphQLName("reactor"), GraphQLNonNullType]
    public Task<ReactorDTO> GetReactorAsync([Service] ReactorByIdDataLoader reactorByIdDataLoader)
        => reactorByIdDataLoader.LoadAsync(ReactorId);

    [GraphQLName("combinedInputPrice")]
    public async Task<double> GetCombinedInputPrice(
        string comex,
        [Service] MaterialByIdDataLoader materialByIdDataLoader,
        [Service] BrokerByTickerDataLoader brokerByTickerDataLoader
    )
    {
        double d = 0;
        foreach (var i in Inputs)
            d += (await (await i.GetMaterialAsync(materialByIdDataLoader)).GetBrokerAtAsync(comex,
                brokerByTickerDataLoader))?.PriceAverage?.Amount ?? Double.NaN;
        return d;
    }

    [GraphQLName("combinedOutputPrice")]
    public async Task<double> GetCombinedOutputPrice(
        string comex,
        [Service] MaterialByIdDataLoader materialByIdDataLoader,
        [Service] BrokerByTickerDataLoader brokerByTickerDataLoader
    )
    {
        double d = 0;
        foreach (var i in Outputs)
            d += (await (await i.GetMaterialAsync(materialByIdDataLoader)).GetBrokerAtAsync(comex,
                brokerByTickerDataLoader))?.PriceAverage?.Amount ?? Double.NaN;
        return d;
    }
    
    public static RecipeDTO Parse(JsonElement jsonElement)
    {
        return new()
        {
            Inputs =
                jsonElement.GetProperty("inputs").EnumerateArray().Select(MaterialQuantityDTO.Parse).ToArray(),
            Outputs =
                jsonElement.GetProperty("outputs").EnumerateArray().Select(MaterialQuantityDTO.Parse).ToArray(),
            Duration = jsonElement.GetProperty("duration").GetProperty("millis").GetInt64(),
            ReactorId = jsonElement.GetProperty("reactorId").GetString() ??
                        ParsingUtils.ThrowRequiredPropertyMissing("reactorId")
        };
    }

    public static RecipeDTO Parse(JsonElement jsonElement, string reactorId)
    {
        return new()
        {
            Inputs =
                jsonElement.GetProperty("inputs").EnumerateArray().Select(MaterialQuantityDTO.Parse).ToArray(),
            Outputs =
                jsonElement.GetProperty("outputs").EnumerateArray().Select(MaterialQuantityDTO.Parse).ToArray(),
            Duration = jsonElement.GetProperty("duration").GetProperty("millis").GetInt64(),
            ReactorId = reactorId
        };
    }
}