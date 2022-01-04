using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("CurrencyAmount")]
[Authorize(Policy = "read:pu_currency_amount")]
public sealed class CurrencyAmountDTO
{
    [GraphQLNonNullType]
    public string Currency { get; set; }

    public double Amount { get; set; }

    public static CurrencyAmountDTO Parse(JsonElement jsonElement)
    {
        return new CurrencyAmountDTO
        {
            Currency = jsonElement.GetProperty("currency").GetString() ??
                       ParsingUtils.ThrowRequiredPropertyMissing("currency"),
            Amount = jsonElement.GetProperty("amount").GetDouble()
        };
    }
}