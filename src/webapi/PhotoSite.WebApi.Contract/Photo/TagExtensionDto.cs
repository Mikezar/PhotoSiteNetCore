using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Photo
{
    public class TagExtensionDto
    {
        [JsonPropertyName("photo_count")]
        public int PhotoCount { get; set; }
    }
}