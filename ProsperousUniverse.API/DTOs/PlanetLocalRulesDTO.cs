using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("PlanetLocalRules")]
[Authorize(Policy = "read:pu_planet_local_rules")]
public sealed class PlanetLocalRulesDTO
{
    [GraphQLNonNullType]
    public CurrencyDTO Currency { get; set; }
    
    // TODO: Governing Entities, see above
    // (collector & governingEntityTypes[])
        
    [GraphQLNonNullType]
    public ProductionFeeDTO[] ProductionFees { get; set; }

    public int LocalMarketBaseFee { get; set; }
    public double LocalMarketTimeFactor { get; set; }
    public int WarehouseFee { get; set; }

    public static PlanetLocalRulesDTO Parse(JsonElement jsonElement)
    {
        var l = jsonElement.GetProperty("localMarketFee");
        return new PlanetLocalRulesDTO
        {
            Currency = CurrencyDTO.Parse(jsonElement.GetProperty("currency")),
            ProductionFees = jsonElement.GetProperty("productionFees").EnumerateArray().Select(ProductionFeeDTO.Parse).ToArray(),
            LocalMarketBaseFee = l.GetProperty("base").GetInt32(),
            LocalMarketTimeFactor = l.GetProperty("timeFactor").GetDouble(),
            WarehouseFee = l.GetProperty("warehouseFee").GetProperty("fee").GetInt32()
        };
    }
}