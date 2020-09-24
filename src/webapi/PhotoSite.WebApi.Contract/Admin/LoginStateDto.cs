using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Admin
{
    public class LoginStateDto
    {
        [JsonPropertyName("status")]
        public LoginStatusDto Status { get; }

        [JsonPropertyName("token")]
        public string Token { get; }

        //public LoginStateDto(LoginStatusDto status, string token)
        //{
        //    Status = status;
        //    Token = token;
        //}
    }
}