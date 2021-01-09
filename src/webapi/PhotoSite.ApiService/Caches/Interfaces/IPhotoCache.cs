using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.Core.Cache;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Caches.Interfaces
{
    public interface IPhotoCache : ICache
    {
        /// <summary>
        /// Get all photos by album's id
        /// </summary>
        /// <returns>Photos</returns>
        Task<IEnumerable<Photo>?> GetByAlbum(int albumId);
    }
}