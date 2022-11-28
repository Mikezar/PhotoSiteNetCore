using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Admin.Authorize
{
    public class LoginDto
    {
        [JsonPropertyName("login")]
        [Required]
        public string Login { get; set; }

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; }
    }
}
