using System.Text.Json;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

public sealed class BrokerDTO // TODO: Make Brokers a root-queryable node
{
    [GraphQLNonNullType, GraphQLType(typeof(IdType))]
    public string Id { get; set; }
    
    [GraphQLNonNullType]
    public string Ticker { get; set; }
    
    [GraphQLIgnore]
    public string ExchangeId { get; set; }

    [GraphQLName("exchange"), GraphQLNonNullType]
    public Task<ComexDTO> GetComexAsync([Service] ComexByIdDataLoader comexByIdDataLoader)
        => comexByIdDataLoader.LoadAsync(ExchangeId);
    
    [GraphQLNonNullType]
    public AddressDTO Address { get; set; }
    
    [GraphQLNonNullType]
    public CurrencyDTO Currency { get; set; }
    
    [GraphQLIgnore]
    public string MaterialId { get; set; }

    [GraphQLName("material"), GraphQLNonNullType]
    public Task<MaterialDTO> GetMaterialAsync([Service] MaterialByIdDataLoader materialByIdDataLoader)
        => materialByIdDataLoader.LoadAsync(MaterialId);

    public CurrencyAmountDTO? Previous { get; set; }

    public CurrencyAmountDTO? Price { get; set; }
    
    public DateTime? PriceTime { get; set; }

    public CurrencyAmountDTO? High { get; set; }

    public CurrencyAmountDTO? AllTimeHigh { get; set; }

    public CurrencyAmountDTO? Low { get; set; }

    public CurrencyAmountDTO? AllTimeLow { get; set; }

    public BrokerOrderDTO? HighestBuyingOrder => BuyingOrders?.MaxBy(x => x.Limit.Amount);
    
    public BrokerOrderDTO? LowestSellingOrder => SellingOrders?.MinBy(x => x.Limit.Amount);

    public int Supply { get; set; }
    
    public int Demand { get; set; }
    
    public int Traded { get; set; }
    
    public CurrencyAmountDTO? Volume { get; set; }

    public CurrencyAmountDTO? PriceAverage { get; set; }

    public PriceBandDTO? NarrowPriceBand { get; set; }
    
    public PriceBandDTO? WidePriceBand { get; set; }

    [GraphQLNonNullType]
    public BrokerOrderDTO[] BuyingOrders { get; set; }
    
    [GraphQLNonNullType]
    public BrokerOrderDTO[] SellingOrders { get; set; }

    public static BrokerDTO Parse(JsonElement jsonElement)
    {
        var v = new BrokerDTO();
        v.Id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id");
        v.Ticker = jsonElement.GetProperty("ticker").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("ticker");
        v.ExchangeId = jsonElement.GetProperty("exchange").GetProperty("id").GetString() ??
                       ParsingUtils.ThrowRequiredPropertyMissing("exchange.id");
        v.Address = AddressDTO.Parse(jsonElement.GetProperty("address"));
        v.Currency = CurrencyDTO.Parse(jsonElement.GetProperty("currency"));
        v.MaterialId = jsonElement.GetProperty("material").GetProperty("id").GetString() ??
                       ParsingUtils.ThrowRequiredPropertyMissing("material.id");
        if (jsonElement.GetProperty("previous").ValueKind != JsonValueKind.Null)
            v.Previous = CurrencyAmountDTO.Parse(jsonElement.GetProperty("previous"));
        
        if (jsonElement.GetProperty("price").ValueKind != JsonValueKind.Null)
            v.Price = CurrencyAmountDTO.Parse(jsonElement.GetProperty("price"));

        if (jsonElement.GetProperty("priceTime").ValueKind != JsonValueKind.Null)
            v.PriceTime = ParsingUtils.ParseUnixWrappedTimestamp(jsonElement.GetProperty("priceTime"));
        
        if (jsonElement.GetProperty("high").ValueKind != JsonValueKind.Null)
            v.High = CurrencyAmountDTO.Parse(jsonElement.GetProperty("high"));
        
        if (jsonElement.GetProperty("low").ValueKind != JsonValueKind.Null)
            v.Low = CurrencyAmountDTO.Parse(jsonElement.GetProperty("low"));
        
        if (jsonElement.GetProperty("allTimeHigh").ValueKind != JsonValueKind.Null)
            v.AllTimeHigh = CurrencyAmountDTO.Parse(jsonElement.GetProperty("allTimeHigh"));
        
        if (jsonElement.GetProperty("allTimeLow").ValueKind != JsonValueKind.Null)
            v.AllTimeLow = CurrencyAmountDTO.Parse(jsonElement.GetProperty("allTimeLow"));
        
        if (jsonElement.GetProperty("priceAverage").ValueKind != JsonValueKind.Null)
            v.PriceAverage = CurrencyAmountDTO.Parse(jsonElement.GetProperty("priceAverage"));
        
        if (jsonElement.GetProperty("widePriceBand").ValueKind != JsonValueKind.Null)
            v.WidePriceBand = PriceBandDTO.Parse(jsonElement.GetProperty("widePriceBand"));
        
        if (jsonElement.GetProperty("narrowPriceBand").ValueKind != JsonValueKind.Null)
            v.NarrowPriceBand = PriceBandDTO.Parse(jsonElement.GetProperty("narrowPriceBand"));
        
        if (jsonElement.GetProperty("volume").ValueKind != JsonValueKind.Null)
            v.Volume = CurrencyAmountDTO.Parse(jsonElement.GetProperty("volume"));

        v.Supply = jsonElement.GetProperty("supply").GetInt32();
        v.Demand = jsonElement.GetProperty("demand").GetInt32();
        v.Traded = jsonElement.GetProperty("traded").GetInt32();

        v.BuyingOrders = jsonElement.GetProperty("buyingOrders").EnumerateArray().Select(BrokerOrderDTO.Parse).ToArray();
        v.SellingOrders = jsonElement.GetProperty("sellingOrders").EnumerateArray().Select(BrokerOrderDTO.Parse).ToArray();

        return v;
    }
}

public sealed class BrokerOrderDTO
{
    [GraphQLNonNullType]
    public string Id { get; set; }
    
    [GraphQLIgnore]
    public string TraderId { get; set; }

    [GraphQLName("trader"), GraphQLNonNullType]
    public Task<UserDTO> GetTraderAsync([Service] UserByIdDataLoader userByIdDataLoader)
        => userByIdDataLoader.LoadAsync(TraderId);

    public int Amount { get; set; }
    
    [GraphQLNonNullType]
    public CurrencyAmountDTO Limit { get; set; }

    public static BrokerOrderDTO Parse(JsonElement jsonElement)
    {
        var amount = jsonElement.GetProperty("amount");
        var a = amount.ValueKind == JsonValueKind.Null ? 0 : amount.GetInt32();
        return new()
        {
            Id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id"),
            TraderId =
                jsonElement.GetProperty("trader").GetProperty("id").GetString() ??
                ParsingUtils.ThrowRequiredPropertyMissing("trader.id"),
            Amount = a,
            Limit = CurrencyAmountDTO.Parse(jsonElement.GetProperty("limit"))
        };
    }
}

public sealed class PriceBandDTO
{
    public double High { get; set; }
    public double Low { get; set; }
    
    public static PriceBandDTO Parse(JsonElement jsonElement)
        => new()
        {
            High = jsonElement.GetProperty("high").GetDouble(),
            Low = jsonElement.GetProperty("low").GetDouble()
        };
}