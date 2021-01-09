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
    public class AlbumCache : SimpleCache<AlbumCache.Container>, IAlbumCache
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumCache(
            IMemoryCache memoryCache,
            IAlbumRepository albumRepository) : base(memoryCache)
        {
            _albumRepository = albumRepository;
        }

        private Task<Container> GetOrAdd()
        {
            return GetOrAdd(async () => new Container((await _albumRepository.GetAll()).ToArray()));
        }

        public async Task<ICollection<Album>?> GetChildren(int? parentAlbumId) =>
            (await GetOrAdd()).ByParentId.TryGetValue(parentAlbumId ?? 0, out var result) ? result : null;

        public async Task<Album?> Get(int albumId) =>
            (await GetOrAdd()).ById.TryGetValue(albumId, out var result) ? result : null;

        public class Container
        {
            public IDictionary<int, Album[]> ByParentId { get; }

            public IDictionary<int, Album> ById { get; }

            public Container(Album[] values)
            {
                ByParentId = values.GroupBy(t => t.ParentId ?? 0)
                    .ToDictionary(t => t.Key, t => t.ToArray());

                ById = values.ToDictionary(t => t.Id);
            }
        }
    }
}