using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PhotoSite.ManagementBoard.Models.Auth
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
