using static PhotoSite.Domain.Admin.TokenManager;

namespace PhotoSite.Domain.Admin
{
    public sealed class LoginState
    {
        public LoginStatus Status { get; }

        public Token? Token { get; }

        private LoginState(LoginStatus status)
        {
            Status = status;
        }

        private LoginState(LoginStatus status, Token token)
        {
            Status = status;
            Token = token;
        }

        public static LoginState ErrorState => new LoginState(LoginStatus.InvalidPasswordOrLogin);
        public static LoginState SuccessState(Token token) => new LoginState(LoginStatus.Success, token);
    }
}