using PhotoSite.ManagementBoard.Models;
using PhotoSite.ManagementBoard.Services.Abstract;
using PhotoSite.WebApi.Admin;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Implementation
{
    internal sealed class SettingsService : ISettingsService
    {
        private const string SettingsEndpoint = "api/adj/settings";
        private readonly IHttpHandler _handler;

        public SettingsService (IHttpHandler handler)
        {
            _handler = handler;
        }

        public async Task<ResultWrapper<SettingsDto>> GetAttributeSettings()
        {
            return await _handler.GetAsync<SettingsDto>(SettingsEndpoint);
        }

        public async Task<NoResultWrapper> SaveAttributeSettings(SettingsDto settings)
        {
            return await _handler.PostAsync(SettingsEndpoint, settings);
        }
    }
}
