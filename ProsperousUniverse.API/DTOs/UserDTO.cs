using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;
using ProsperousUniverse.API.DataLoaders;

namespace ProsperousUniverse.API.DTOs;

[Node, GraphQLName("User")]
[Authorize(Policy = "read:pu_users")]
public sealed class UserDTO : INode
{
    [GraphQLType(typeof(IdType)), GraphQLNonNullType]
    public string Id { get; set; }

    [GraphQLNonNullType]
    public string Username { get; set; }

    [GraphQLNonNullType] public string SubscriptionLevel { get; set; }

    public string? HighestTier { get; set; }
    
    public bool Team { get; set; }
    
    public bool Moderator { get; set; }
    
    public bool Pioneer { get; set; }
    
    public DateTime Created { get; set; }

    [GraphQLIgnore] public string CompanyId { get; set; }

    [GraphQLName("company")]
    public Task<CompanyDTO> GetCompanyAsync([Service] CompanyByIdDataLoader companyByIdDataLoader)
    {
        return companyByIdDataLoader.LoadAsync(CompanyId);
    }

    // There are more fields for the self user, but we ignore them here.    

    [NodeResolver]
    public static Task<UserDTO> Get(string id, [Service] UserByIdDataLoader userByIdDataLoader)
        => userByIdDataLoader.LoadAsync(id);

    public static UserDTO Parse(JsonElement jsonElement)
    {
        var id = jsonElement.GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("id");
        return new UserDTO
        {
            Id = id,
            Username = jsonElement.GetProperty("username").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("username"),
            SubscriptionLevel = jsonElement.GetProperty("subscriptionLevel").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("subscriptionLevel"),
            HighestTier = jsonElement.GetProperty("highestTier").GetString(),
            Team = jsonElement.GetProperty("team").GetBoolean(),
            Moderator = jsonElement.GetProperty("moderator").GetBoolean(),
            Pioneer = jsonElement.GetProperty("pioneer").GetBoolean(),
            Created = ParsingUtils.ParseUnixWrappedTimestamp(jsonElement.GetProperty("created")),
            CompanyId = jsonElement.GetProperty("company").GetProperty("id").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("company.id")
        };
    }
}
