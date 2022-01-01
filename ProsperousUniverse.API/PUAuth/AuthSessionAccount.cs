using System.Text.Json.Serialization;

namespace ProsperousUniverse.API;

public sealed record AuthSessionAccount(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("disposableId")] string DisposableId,
    [property: JsonPropertyName("displayName")] string DisplayName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("preferredLanguage")] string PreferredLanguage,
    [property: JsonPropertyName("registered")] DateTime RegisteredAt,
    [property: JsonPropertyName("confirmed")] DateTime? ConfirmedAt,
    [property: JsonPropertyName("confirmationSource")] string ConfirmationSource,
    [property: JsonPropertyName("deleted")] DateTime? DeletedAt,
    [property: JsonPropertyName("deleteReason")] string DeleteReason,
    [property: JsonPropertyName("roles")] string[] Roles);