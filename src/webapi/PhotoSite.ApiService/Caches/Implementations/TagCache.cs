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
    public class TagCache : SimpleCache<TagCache.Container>, ITagCache
    {
        private readonly ITagRepository _tagRepository;

        public TagCache(
            IMemoryCache memoryCache,
            ITagRepository tagRepository) : base(memoryCache)
        {
            _tagRepository = tagRepository;
        }

        private Task<Container> GetOrAdd()
        {
            return GetOrAdd(async () => new Container((await _tagRepository.GetAll()).ToArray()));
        }

        public async Task<ICollection<Tag>> GetAll() => (await GetOrAdd()).All;

        public class Container
        {
            public ICollection<Tag> All { get; }

            public Container(Tag[] values)
            {
                All = values;
            }
        }
    }
}