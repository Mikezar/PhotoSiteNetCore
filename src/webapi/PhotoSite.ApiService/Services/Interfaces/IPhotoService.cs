using System.Threading.Tasks;
using PhotoSite.ApiService.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Interfaces
{
    public interface IPhotoService : IService
    {
        /// <summary>
        /// Get photo by id
        /// </summary>
        /// <returns>Photo</returns>
        Task<Photo?> Get(int id);
    }
}