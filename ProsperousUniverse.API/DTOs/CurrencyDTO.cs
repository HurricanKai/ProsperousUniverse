using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("Currency")]
[Authorize(Policy = "read:pu_currency")]
public sealed class CurrencyDTO
{
    public int NumericCode { get; set; }
    [GraphQLNonNullType]
    public string Code { get; set; }
    [GraphQLNonNullType]
    public string Name { get; set; }
    public int Decimals { get; set; }

    public static CurrencyDTO Parse(JsonElement jsonElement)
    {
        return new CurrencyDTO
        {
            NumericCode = jsonElement.GetProperty("numericCode").GetInt32(),
            Code = jsonElement.GetProperty("code").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("currency.code"),
            Decimals = jsonElement.GetProperty("decimals").GetInt32(),
            Name = jsonElement.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("currency.name")
        };
    }
}
