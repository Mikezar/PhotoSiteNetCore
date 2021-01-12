using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PhotoSite.ApiService.Caches.Interfaces;
using PhotoSite.Core.Cache;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.ApiService.Caches.Implementations
{
    public class PhotoToTagCache : SimpleCache<PhotoToTagCache.Container>, IPhotoToTagCache
    {
        private readonly IPhotoToTagRepository _photoToTagRepository;

        public PhotoToTagCache(
            IMemoryCache memoryCache,
            IPhotoToTagRepository photoToTagRepository) : base(memoryCache)
        {
            _photoToTagRepository = photoToTagRepository;
        }

        private Task<Container> GetOrAdd()
        {
            return GetOrAdd(async () => new Container((await _photoToTagRepository.GetAll()).ToArray()));
        }

        public async Task<ICollection<PhotoToTag>> GetAll() => (await GetOrAdd()).All;

        public class Container
        {
            public ICollection<PhotoToTag> All { get; }

            public Container(PhotoToTag[] values)
            {
                All = values;
            }
        }
    }
}