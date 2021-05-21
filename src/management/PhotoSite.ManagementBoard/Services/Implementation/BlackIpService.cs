using PhotoSite.ManagementBoard.Models;
using PhotoSite.ManagementBoard.Services.Abstract;
using PhotoSite.WebApi.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Implementation
{
    internal sealed class BlackIpService : IBlackIpService
    {
        private static string BlackIpEndpoint = "api/blackip";

        private readonly IHttpHandler _httpHandler;

        public BlackIpService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task<ResultWrapper<IList<BlackIpDto>>> GetAllIpsAsync()
        {
            return await _httpHandler.GetAsync<IList<BlackIpDto>>(BlackIpEndpoint);
        }
    }
}
