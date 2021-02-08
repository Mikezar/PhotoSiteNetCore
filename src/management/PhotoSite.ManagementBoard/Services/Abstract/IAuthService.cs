using PhotoSite.ManagementBoard.Models.Auth;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Abstract
{
    internal interface IAuthService : IService
    {
        Task<bool> SignInAsync(LoginModel loginModel);
        Task SignOutAsync();
    }
}
