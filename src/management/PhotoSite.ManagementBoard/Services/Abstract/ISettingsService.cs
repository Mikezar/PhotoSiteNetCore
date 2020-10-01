using PhotoSite.ManagementBoard.Models;
using PhotoSite.WebApi.Admin;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Abstract
{
    public interface ISettingsService
    {
        Task<ResultWrapper<SettingsDto>> GetAttributeSettings();
        Task<NoResultWrapper> SaveAttributeSettings(SettingsDto settings);
    }
}
