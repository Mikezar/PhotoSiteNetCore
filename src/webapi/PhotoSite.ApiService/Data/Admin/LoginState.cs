namespace PhotoSite.ApiService.Data.Admin
{
    public class LoginState
    {
        public LoginStatus Status { get; }

        public string Token { get; }

        public LoginState(LoginStatus status, string token)
        {
            Status = status;
            Token = token;
        }

        public static LoginState GetErrorState(LoginStatus status) => new LoginState(status, null);
    }
}