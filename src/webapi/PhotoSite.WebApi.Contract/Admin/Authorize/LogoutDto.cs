using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Admin.Authorize
{
    public class LogoutDto
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
