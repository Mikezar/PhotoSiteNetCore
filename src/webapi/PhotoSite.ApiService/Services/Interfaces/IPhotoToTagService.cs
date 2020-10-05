using System.Threading.Tasks;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Interfaces
{
    public interface IPhotoToTagService : IService
    {
        /// <summary>
        /// Get entities by photo's id
        /// </summary>
        /// <returns>All tags</returns>
        Task<PhotoToTag[]> GetByPhotoId(int photoId);

        /// <summary>
        /// Get tag's ids not in photo
        /// </summary>
        /// <returns>All tags</returns>
        Task<int[]> GetNotExistsInPhoto(int photoId);

        /// <summary>
        /// Bind tags to photo
        /// </summary>
        Task<Result> BindTagsToPhoto(int photoId, int[] tagIds);
    }
}