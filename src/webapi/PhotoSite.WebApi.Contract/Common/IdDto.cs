using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Common
{
    public class IdDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}