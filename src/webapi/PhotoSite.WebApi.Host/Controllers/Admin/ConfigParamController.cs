using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.WebApi.Admin;

namespace PhotoSite.WebApi.Controllers.Admin
{
    [Produces("application/json")]
    [Route("api/adj")]
    [ApiController]
    [Authorize]
    public class ConfigParamController : ControllerBase
    {
        private readonly IConfigParamService _settingService;
        private readonly IMapper _mapper;

        public ConfigParamController(IConfigParamService settingService, IMapper mapper)
        {
            _settingService = settingService;
            _mapper = mapper;
        }

        [HttpGet("default")]
        public ConfigParamDto GetDefaultSettings()
        {
            var result = _settingService.GetDefaultSettings();
            return _mapper.Map<ConfigParamDto>(result);
        }

        [HttpGet("settings")]
        public async Task<ConfigParamDto> GetSettings()
        {
            var result = await _settingService.GetSettings();
            return _mapper.Map<ConfigParamDto>(result);
        }

        [HttpPost("settings")]
        public async Task SetSettings([FromBody] ConfigParamDto settingsDto)
        {
            var settings = _mapper.Map<Settings>(settingsDto);
            await _settingService.SaveSettings(settings);
        }
    }
}