using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoSite.ApiService.Caches.Interfaces;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Services.Implementations;
using PhotoSite.Core.Cache;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.ApiService.Caches.Implementations
{
    public class BlackIpCache : SimpleCache<string, BlackIpCache.Container>, IBlackIpCache
    {
        private readonly IBlackIpRepository _blackIpRepository;

        public BlackIpCache(
            MemoryCacheProvider memoryCacheProvider, 
            IBlackIpRepository blackIpRepository) : base(memoryCacheProvider, "def")
        {
            _blackIpRepository = blackIpRepository;
        }

        private Task<Container> GetOrAdd()
        {
            return GetOrAdd(async () => new Container((await _blackIpRepository.GetAll()).ToArray()));
        }

        public async Task<IEnumerable<BlackInterNetworkV4>> GetV4() => (await GetOrAdd()).V4;

        public async Task<IEnumerable<BlackInterNetworkV6>> GetV6() => (await GetOrAdd()).V6;

        public class Container
        {
            public IEnumerable<BlackInterNetworkV4> V4 { get; }

            public IEnumerable<BlackInterNetworkV6> V6 { get; }

            public Container(BlackIp[] values)
            {
                V4 = values.Where(t => !t.IsInterNetworkV6).Select(BlackIpService.CreateBlackInterNetworkV4);
                V6 = values.Where(t => t.IsInterNetworkV6).Select(BlackIpService.CreateBlackInterNetworkV6);
            }
        }
    }
}