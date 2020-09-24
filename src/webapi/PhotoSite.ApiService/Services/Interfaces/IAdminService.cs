using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data.Admin;

namespace PhotoSite.ApiService.Services.Interfaces
{
    public interface IAdminService : IService
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <returns>Login state</returns>
        LoginState Login(string login, string password);

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="token">Token</param>
        void Logout(string token);
    }
}