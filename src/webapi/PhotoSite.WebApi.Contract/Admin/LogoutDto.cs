using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Admin
{
    public class LogoutDto
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
