using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Photo
{
    public class TagTitleDto
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}