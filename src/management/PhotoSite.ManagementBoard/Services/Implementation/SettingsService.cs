using PhotoSite.ManagementBoard.Models;
using PhotoSite.ManagementBoard.Services.Abstract;
using PhotoSite.WebApi.Admin;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Implementation
{
    internal sealed class SettingsService : ISettingsService
    {
        private const string SettingsEndpoint = "api/adj/settings";
        private const string DefaultSettingsEndpoint = "api/adj/default";

        private readonly IHttpHandler _handler;

        public SettingsService (IHttpHandler handler)
        {
            _handler = handler;
        }

        public async Task<ResultWrapper<ConfigParamDto>> GetAttributeSettings()
        {
            return await _handler.GetAsync<ConfigParamDto>(SettingsEndpoint);
        }

        public async Task<ResultWrapper<ConfigParamDto>> GetDefaultSettings()
        {
            return await _handler.GetAsync<ConfigParamDto>(DefaultSettingsEndpoint);
        }

        public async Task<NoResultWrapper> SaveAttributeSettings(ConfigParamDto settings)
        {
            return await _handler.PostAsync(SettingsEndpoint, settings);
        }
    }
}
