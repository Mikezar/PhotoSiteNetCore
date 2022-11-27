using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.WebApi.Admin.Authorize;
using PhotoSite.WebApi.Infrastructure.Authorization;

namespace PhotoSite.WebApi.Controllers.Admin
{
    [Produces("application/json")]
    [Route("api/ad")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOptionsMonitor<CustomTokenAuthOptions> _options;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IOptionsMonitor<CustomTokenAuthOptions> options, IAdminService adminService, IMapper mapper)
        {
            _options = options;
            _adminService = adminService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public LoginStateDto Login([FromBody]LoginDto dto)
        {
            var result = _adminService.Login(dto.Login, dto.Password);
            return _mapper.Map<LoginStateDto>(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public void Logout()
        {
            var token = Request.Headers[_options.CurrentValue.TokenHeaderName];
            _adminService.Logout(token);
        }
    }
}