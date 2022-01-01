using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[Node, GraphQLName("Country")]
[Authorize(Policy = "read:pu_country")]
public sealed class CountryDTO : INode
{
    [GraphQLType(typeof(IdType)), GraphQLNonNullType]
    public string Id { get; set; }

    [GraphQLNonNullType]
    public string Code { get; set; }

    [GraphQLNonNullType]
    public string Name { get; set; }

    [GraphQLNonNullType]
    public CurrencyDTO Currency { get; set; }


    [NodeResolver]
    public static Task<CountryDTO> Get(string id, [Service] CountryByIdDataLoader countryByIdDataLoader)
        => countryByIdDataLoader.LoadAsync(id);

    public static CountryDTO Parse(JsonElement jsonElement)
    {
        return new CountryDTO
        {
            Id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id"),
            Code = jsonElement.GetProperty("code").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("code"),
            Name = jsonElement.GetProperty("name").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("name"),
            Currency = CurrencyDTO.Parse(jsonElement.GetProperty("currency"))
        };
    }
}
