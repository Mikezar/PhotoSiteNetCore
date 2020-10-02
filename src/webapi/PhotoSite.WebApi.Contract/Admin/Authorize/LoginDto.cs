using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Admin.Authorize
{
    public class LoginDto
    {
        [JsonPropertyName("login")]
        public string? Login { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
