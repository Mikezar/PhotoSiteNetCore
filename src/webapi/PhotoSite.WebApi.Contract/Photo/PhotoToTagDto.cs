using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Photo
{
    public class PhotoToTagDto
    {
        [JsonPropertyName("photo_id")]
        public int PhotoId { get; set; }

        [JsonPropertyName("tag_id")]
        public int TagId { get; set; }
    }
}