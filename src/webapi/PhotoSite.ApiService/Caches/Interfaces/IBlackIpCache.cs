using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.ApiService.Data;
using PhotoSite.Core.Cache;

namespace PhotoSite.ApiService.Caches.Interfaces
{
    public interface IBlackIpCache : ICache
    {
        Task<IEnumerable<BlackInterNetworkV4>> GetV4();

        Task<IEnumerable<BlackInterNetworkV6>> GetV6();
    }
}