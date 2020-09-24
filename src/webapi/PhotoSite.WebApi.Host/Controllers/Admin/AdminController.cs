using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.WebApi.Admin;

namespace PhotoSite.WebApi.Controllers.Admin
{
    /// <summary>
    /// Admin
    /// </summary>
    [Produces("application/json")]
    [Route("api/ad")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public AdminController(IAdminService adminService, IMapper mapper)
        {
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
        /// <param name="dto"><see cref="LogoutDto"/></param>
        [HttpPost("logout")]
        public void Logout([FromBody]LogoutDto dto)
        {
            _adminService.Logout(dto.Token);
        }
    }
}