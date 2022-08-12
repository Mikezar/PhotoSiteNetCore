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
    /// <summary>
    /// Black ip
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class BlackIpController : CustomControllerBase
    {
        private readonly IBlackIpService _service;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public BlackIpController(IBlackIpService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Get all black IP's
        /// </summary>
        /// <returns>All IP's</returns>
        [HttpGet("all")]
        public async Task<BlackIpDto[]> All()
        {
            var result = await _service.GetAll();
            return _mapper.Map<BlackIpDto[]>(result);
        }

        /// <summary>
        /// Add new black IP
        /// </summary>
        /// <param name="blackIp">Black IP</param>
        /// <returns>Identification new black IP</returns>
        [HttpPost]
        public async Task<IdResultDto> Create([FromBody] BlackIpDto? blackIp)
        {
            Validate(blackIp);
            var value = _mapper.Map<BlackIp>(blackIp);
            var result = await _service.Create(value);
            return _mapper.Map<IdResultDto>(result);
        }

        /// <summary>
        /// Update black IP
        /// </summary>
        /// <param name="blackIp">Black IP</param>
        [HttpPut]
        public async Task Update([FromBody] BlackIpDto? blackIp)
        {
            Validate(blackIp);
            var value = _mapper.Map<BlackIp>(blackIp);
            await _service.Update(value);
        }

        /// <summary>
        /// Delete black IP
        /// </summary>
        /// <param name="id">Black IP identification</param>
        /// <returns>Result</returns>
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