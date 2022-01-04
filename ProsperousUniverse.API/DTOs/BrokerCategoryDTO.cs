using System.Text.Json;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

public sealed class BrokerCategoryDTO
{
    [GraphQLIgnore]
    public string CategoryId { get; set; }

    [GraphQLName("category"), GraphQLNonNullType]
    public Task<MaterialCategoryDTO> GetCategoryAsync(
        [Service] MaterialCategoryByIdDataLoader materialCategoryByIdDataLoader)
        => materialCategoryByIdDataLoader.LoadAsync(CategoryId);
    
    [GraphQLIgnore]
    public string ExchangeId { get; set; }

    [GraphQLName("exchange"), GraphQLNonNullType]
    public Task<ComexDTO> GetExchangeId(
        [Service] ComexByIdDataLoader comexByIdDataLoader)
        => comexByIdDataLoader.LoadAsync(ExchangeId);

    [GraphQLIgnore]
    public string[] BrokerTickers { get; set; }
    
    [GraphQLName("brokers"), GraphQLNonNullType]
    [UsePaging]
    public async IAsyncEnumerable<BrokerDTO> GetBrokersAsync([Service] BrokerByTickerDataLoader brokerByTickerDataLoader)
    {
        foreach (var ticker in BrokerTickers)
        {
            yield return await brokerByTickerDataLoader.LoadAsync(ticker);
        }
    }

    public static BrokerCategoryDTO Parse(JsonElement jsonElement)
        => new()
        {
            CategoryId =
                jsonElement.GetProperty("categoryId").GetString() ??
                ParsingUtils.ThrowRequiredPropertyMissing("categoryId"),
            ExchangeId =
                jsonElement.GetProperty("exchangeId").GetString() ??
                ParsingUtils.ThrowRequiredPropertyMissing("exchangeId"),
            BrokerTickers = jsonElement.GetProperty("data").EnumerateArray().Select(x
                    => x.GetProperty("ticker").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("ticker"))
                .ToArray()
        };
}
