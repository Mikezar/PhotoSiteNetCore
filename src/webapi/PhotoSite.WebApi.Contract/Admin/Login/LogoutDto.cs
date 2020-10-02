using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Admin.Login
{
    public class LogoutDto
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
