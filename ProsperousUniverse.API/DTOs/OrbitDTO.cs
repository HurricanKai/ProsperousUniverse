using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("Orbit")]
[Authorize(Policy = "read:pu_orbit")]
public sealed class OrbitDTO
{
    public double SemiMajorAxis { get; set; }
    public double Eccentricity { get; set; }
    public double Inclination { get; set; }
    public double RightAscension { get; set; }
    public double Periapsis { get; set; }

    public static OrbitDTO Parse(JsonElement jsonElement)
    {
        return new OrbitDTO()
        {
            SemiMajorAxis = jsonElement.GetProperty("semiMajorAxis").GetDouble(),
            Eccentricity = jsonElement.GetProperty("eccentricity").GetDouble(),
            Inclination = jsonElement.GetProperty("inclination").GetDouble(),
            RightAscension = jsonElement.GetProperty("rightAscension").GetDouble(),
            Periapsis = jsonElement.GetProperty("periapsis").GetDouble()
        };
    }
}