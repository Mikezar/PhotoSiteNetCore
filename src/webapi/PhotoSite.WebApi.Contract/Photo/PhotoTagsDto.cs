using System.Text.Json.Serialization;
using PhotoSite.WebApi.Common;

namespace PhotoSite.WebApi.Photo
{
    public class PhotoTagsDto
    {
        [JsonPropertyName("photo_id")]
        public int PhotoId { get; set; }

        [JsonPropertyName("tags")]
        public IdResultDto[]? TagIds { get; set; }
    }
}