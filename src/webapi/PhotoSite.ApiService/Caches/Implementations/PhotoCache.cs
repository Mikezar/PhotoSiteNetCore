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
    public class PhotoCache : SimpleCache<PhotoCache.Container>, IPhotoCache
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoCache(
            IMemoryCache memoryCache,
            IPhotoRepository photoRepository) : base(memoryCache)
        {
            _photoRepository = photoRepository;
        }

        private Task<Container> GetOrAdd()
        {
            return GetOrAdd(async () => new Container((await _photoRepository.GetAll()).ToArray()));
        }

        public async Task<IEnumerable<Photo>?> GetByAlbum(int albumId) =>
            (await GetOrAdd()).ByAlbumId.TryGetValue(albumId, out var result) ? result : null;

        public class Container
        {
            public IDictionary<int, IEnumerable<Photo>> ByAlbumId { get; }

            public Container(Photo[] values)
            {
                ByAlbumId = values.GroupBy(t => t.AlbumId)
                    .ToDictionary(t => t.Key, t => t.AsEnumerable());
            }
        }
    }
}