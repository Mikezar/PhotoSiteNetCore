namespace PhotoSite.WebApi.Admin
{
    public class LoginStateDto
    {
        public LoginStatusDto Status { get; }

        public string Token { get; }

        public LoginStateDto(LoginStatusDto status, string token)
        {
            Status = status;
            Token = token;
        }
    }
}