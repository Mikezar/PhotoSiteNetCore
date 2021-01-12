using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.Core.Cache;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Caches.Interfaces
{
    public interface IPhotoToTagCache : ICache
    {
        Task<ICollection<PhotoToTag>> GetAll();
    }
}