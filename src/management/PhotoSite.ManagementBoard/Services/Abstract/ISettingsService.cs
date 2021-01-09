using PhotoSite.ManagementBoard.Models;
using PhotoSite.WebApi.Admin;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Abstract
{
    public interface ISettingsService
    {
        Task<ResultWrapper<ConfigParamDto>> GetAttributeSettings();
        Task<ResultWrapper<ConfigParamDto>> GetDefaultSettings();
        Task<NoResultWrapper> SaveAttributeSettings(ConfigParamDto settings);
    }
}
