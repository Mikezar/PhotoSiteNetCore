using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Photo
{
    public class TagDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}