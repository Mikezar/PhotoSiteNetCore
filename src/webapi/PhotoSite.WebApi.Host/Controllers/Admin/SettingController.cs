using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.WebApi.Admin;

namespace PhotoSite.WebApi.Controllers.Admin
{
    /// <summary>
    /// Settings
    /// </summary>
    [Route("api/adj")]
    [Authorize]
    public class SettingController : BaseController
    {
        private readonly ISettingService _settingService;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public SettingController(ISettingService settingService, IMapper mapper)
        {
            _settingService = settingService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get default settings
        /// </summary>
        /// <returns>Default settings</returns>
        [HttpGet("default")]
        public SettingsDto GetDefaultSettings()
        {
            var result = _settingService.GetDefaultSettings();
            return _mapper.Map<SettingsDto>(result);
        }

        /// <summary>
        /// Get settings
        /// </summary>
        /// <returns>Settings</returns>
        [HttpGet("settings")]
        public async Task<SettingsDto> GetSettings()
        {
            var result = await _settingService.GetSettings();
            return _mapper.Map<SettingsDto>(result);
        }

        /// <summary>
        /// Set settings
        /// </summary>
        /// <returns>Settings</returns>
        [HttpPost("settings")]
        public async Task SetSettings([FromBody] SettingsDto settingsDto)
        {
            var settings = _mapper.Map<Settings>(settingsDto);
            await _settingService.SaveSettings(settings);
        }
    }
}