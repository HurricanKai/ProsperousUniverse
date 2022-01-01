using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProsperousUniverse.API.JsonModels;

public sealed record BaseEvent([property: JsonPropertyName("messageType")] string MessageType, [property: JsonPropertyName("payload")] JsonElement Payload);