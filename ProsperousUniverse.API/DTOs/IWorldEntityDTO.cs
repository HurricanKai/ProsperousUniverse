namespace ProsperousUniverse.API.DTOs;

[GraphQLName("WorldEntity")]
public interface IWorldEntityDTO
{
    [GraphQLNonNullType, GraphQLType(typeof(IdType))]
    string Id { get; set; }
    
    [GraphQLNonNullType]
    string Name { get; set; }
    
    [GraphQLNonNullType]
    string NaturalId { get; set; }
    
    [GraphQLNonNullType]
    public AddressDTO Address { get; set; }
}
