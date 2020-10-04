using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Common
{
    public class ResultDto
    {
        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }
    }
}