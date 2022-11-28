using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhotoSite.Application.Authorization;
using PhotoSite.WebApi.Admin.Authorize;
using PhotoSite.WebApi.Infrastructure.Authorization;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoSite.WebApi.Controllers.Admin
{
    [Produces("application/json")]
    [Route("api/ad")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOptionsMonitor<CustomTokenAuthOptions> _tokenAuthOptions;
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public AdminController(
            IOptionsMonitor<CustomTokenAuthOptions> tokenAuthOptions,
            ISender sender,
            IMapper mapper)
        {
            _tokenAuthOptions = tokenAuthOptions;
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<LoginStateDto> Login([FromBody]LoginDto dto, CancellationToken cancellationToken)
        {
            var command = new LoginCommand(dto.Login, dto.Password);
            var result = await _sender.Send(command, cancellationToken);
            return _mapper.Map<LoginStateDto>(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task Logout(CancellationToken cancellationToken)
        {
            var token = Request.Headers[_tokenAuthOptions.CurrentValue.TokenHeaderName];
            var command = new LogoutCommand(token);
            await _sender.Send(command, cancellationToken);
        }
    }
}