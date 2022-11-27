using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Core.ExtException;
using PhotoSite.Data.Entities;
using PhotoSite.WebApi.Admin;
using PhotoSite.WebApi.Common;

namespace PhotoSite.WebApi.Controllers.Admin
{
    [Produces("application/json")]
    [Route("api/blackIps")]
    [ApiController]
    [Authorize]
    public class BlackIpController : ControllerBase
    {
        private readonly IBlackIpService _service;
        private readonly IMapper _mapper;

        public BlackIpController(IBlackIpService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<BlackIpDto[]> All()
        {
            var result = await _service.GetAll();
            return _mapper.Map<BlackIpDto[]>(result);
        }

        [HttpPost]
        public async Task<IdResultDto> Create([FromBody] BlackIpDto? blackIp)
        {
            Validate(blackIp);
            var value = _mapper.Map<BlackIp>(blackIp);
            var result = await _service.Create(value);
            return _mapper.Map<IdResultDto>(result);
        }

        [HttpPut]
        public async Task Update([FromBody] BlackIpDto? blackIp)
        {
            Validate(blackIp);
            var value = _mapper.Map<BlackIp>(blackIp);
            await _service.Update(value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Delete(id);
        }

        private void Validate(BlackIpDto? blackIp)
        {
            if (blackIp is null)
                throw new UserException("Black Ip cannot be empty");
        }
    }
}