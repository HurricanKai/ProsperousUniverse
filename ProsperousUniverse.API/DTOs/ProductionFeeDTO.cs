using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("ProductionFee")]
[Authorize(Policy = "read:pu_production_fee")]
public sealed class ProductionFeeDTO
{
    public string Category { get; set; } // TODO: Can we resolve material categories to something more useful?
    public CurrencyAmountDTO Fee { get; set; }

    public static ProductionFeeDTO Parse(JsonElement jsonElement)
    {
        return new ProductionFeeDTO()
        {
            Category = jsonElement.GetProperty("category").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("category"),
            Fee = CurrencyAmountDTO.Parse(jsonElement.GetProperty("fee"))
        };
    }
}