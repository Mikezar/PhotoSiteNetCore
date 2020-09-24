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
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <returns>Login state</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public LoginStateDto Login(string login, string password)
        {
            var result = _adminService.Login(login, password);
            return _mapper.Map<LoginStateDto>(result);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="token">Token</param>
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Logout(string token)
        {
            _adminService.Logout(token);
        }
    }
}