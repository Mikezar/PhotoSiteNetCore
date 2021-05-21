using PhotoSite.ManagementBoard.Models;
using PhotoSite.WebApi.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Abstract
{
    public interface IBlackIpService : IService
    {
        Task<ResultWrapper<IList<BlackIpDto>>> GetAllIpsAsync();
    }
}
