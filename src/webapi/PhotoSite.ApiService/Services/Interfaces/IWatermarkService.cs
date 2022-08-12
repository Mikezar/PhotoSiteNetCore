using System.Threading.Tasks;
using PhotoSite.ApiService.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Interfaces
{
    public interface IWatermarkService : IService
    {
        /// <summary>
        /// Get entity by photo's id
        /// </summary>
        /// <returns>All tags</returns>
        Task<Watermark?> GetByPhotoId(int photoId);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="watermark">Entity</param>
        Task Update(Watermark watermark);

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="watermark">Entity</param>
        Task Create(Watermark watermark);
    }
}