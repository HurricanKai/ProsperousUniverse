using System.Text.Json;
using HotChocolate.AspNetCore.Authorization;

namespace ProsperousUniverse.API.DTOs;

[GraphQLName("RatingReport")]
[Authorize(Policy = "read:pu_rating_report")]
public sealed class RatingReportDTO
{
    [GraphQLNonNullType]
    public string OverallRating { get; set; }

    [GraphQLNonNullType]
    public SubRatingDTO[] SubRatings { get; set; }

    public static RatingReportDTO Parse(JsonElement jsonElement)
    {
        return new RatingReportDTO()
        {
            OverallRating = jsonElement.GetProperty("overallRating").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("overallRating"),
            SubRatings = jsonElement.GetProperty("subRatings").EnumerateArray().Select(SubRatingDTO.Parse).ToArray()
        };
    }
    
    public sealed class SubRatingDTO
    {
        [GraphQLNonNullType]
        public string Score { get; set; }

        [GraphQLNonNullType]
        public string Rating { get; set; }

        public static SubRatingDTO Parse(JsonElement jsonElement)
        {
            return new SubRatingDTO()
            {
                Score = jsonElement.GetProperty("score").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("score"),
                Rating = jsonElement.GetProperty("rating").GetString() ?? ParsingUtils.ThrowRequiredPropertyMissing("rating"),
            };
        }
    }
}
