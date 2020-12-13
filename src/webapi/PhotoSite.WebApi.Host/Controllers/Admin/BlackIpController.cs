using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Entities;
using PhotoSite.WebApi.Admin;
using PhotoSite.WebApi.Common;

namespace PhotoSite.WebApi.Controllers.Admin
{
    /// <summary>
    /// Black ip
    /// </summary>
    [Route("api/blacklist")]
    [Authorize]
    public class BlackIpController : BaseController
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
        [HttpGet("getall")]
        public async Task<BlackIpDto[]> GetAll()
        {
            var result = await _service.GetAll();
            return _mapper.Map<BlackIpDto[]>(result);
        }

        /// <summary>
        /// Add new black IP
        /// </summary>
        /// <param name="blackIp">Black IP</param>
        /// <returns>Identification new black IP</returns>
        [HttpPost("create")]
        public async Task<IdResultDto> Create(BlackIpDto? blackIp)
        {
            if (blackIp == null)
                return new IdResultDto { ErrorMessage = "Black Ip cannot be empty" };
            var value = _mapper.Map<BlackIp>(blackIp);
            var result = await _service.Create(value);
            return _mapper.Map<IdResultDto>(result);
        }

        /// <summary>
        /// Update black IP
        /// </summary>
        /// <param name="blackIp">Black IP</param>
        [HttpPost("update")]
        public async Task<ResultDto> Update(BlackIpDto? blackIp)
        {
            if (blackIp == null)
                return new IdResultDto { ErrorMessage = "Black Ip cannot be empty" };
            var value = _mapper.Map<BlackIp>(blackIp);
            var result = await _service.Update(value);
            return _mapper.Map<ResultDto>(result);
        }

        /// <summary>
        /// Delete black IP
        /// </summary>
        /// <param name="id">Black IP identification</param>
        /// <returns>Result</returns>
        [HttpGet("delete")]
        public async Task<ResultDto> Delete(int id)
        {
            var result = await _service.Delete(id);
            return _mapper.Map<ResultDto>(result);
        }

    }
}