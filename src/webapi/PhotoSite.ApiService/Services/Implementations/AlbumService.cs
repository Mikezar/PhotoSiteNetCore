using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        /// <summary>
        /// Get child albums
        /// </summary>
        /// <param name="parentId">Parent album's identification</param>
        /// <returns>Albums</returns>
        public async Task<ICollection<Album>> GetChild(int? parentId)
        {
            return await _albumRepository.GetAsNoTrackingChild(parentId);
        }

    }
}