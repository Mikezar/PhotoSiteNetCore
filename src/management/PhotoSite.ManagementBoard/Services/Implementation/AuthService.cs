using PhotoSite.ManagementBoard.Models.Auth;
using PhotoSite.ManagementBoard.Services.Abstract;
using PhotoSite.WebApi.Admin;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Implementation
{
    internal sealed class AuthService : IAuthService
    {
        private const string LogInMethod = "/api/ad/login";
        private const string LogOutMethod = "/api/ad/logout";

        private readonly IHttpHandler _httpHandler;

        public AuthService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task<bool> SignIn(LoginModel loginModel)
        {
            var response = await _httpHandler.PostAsync<LoginStateDto>(LogInMethod, new LoginDto
            {
                Login = loginModel.Username,
                Password = loginModel.Password
            });

            if (response.IsSuccess &&
                response.Result.Status == LoginStatusDto.Success)
            {
                _httpHandler.Token = response.Result.Token;
                return true;
            }

            return false;
        }

        public async Task SignOut()
        {
            await _httpHandler.PostAsync(LogOutMethod, 
                new LogoutDto {
                    Token = _httpHandler.Token 
                });

            _httpHandler.Token = null;
        }
    }
}
