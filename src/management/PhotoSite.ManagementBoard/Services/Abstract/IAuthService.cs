using PhotoSite.ManagementBoard.Models.Auth;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Abstract
{
    internal interface IAuthService
    {
        Task<bool> SignIn(LoginModel loginModel);
        Task SignOut();
    }
}
