using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.WebApi.Admin.Authorize;
using PhotoSite.WebApi.Options;

namespace PhotoSite.WebApi.Controllers.Admin
{
    /// <summary>
    /// Admin
    /// </summary>
    [Route("api/ad")]
    public class AdminController : BaseController
    {
        private readonly IOptionsMonitor<CustomTokenAuthOptions> _options;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public AdminController(IOptionsMonitor<CustomTokenAuthOptions> options, IAdminService adminService, IMapper mapper)
        {
            _options = options;
            _adminService = adminService;
            _mapper = mapper;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="dto"><see cref="LoginDto"/></param>
        /// <returns>Login state</returns>
        [HttpPost("login")]
        public LoginStateDto Login([FromBody]LoginDto dto)
        {
            var result = _adminService.Login(dto.Login, dto.Password);
            return _mapper.Map<LoginStateDto>(result);
        }

        /// <summary>
        /// Logout
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        public void Logout()
        {
            var token = Request.Headers[_options.CurrentValue.TokenHeaderName];
            _adminService.Logout(token);
        }
    }
}