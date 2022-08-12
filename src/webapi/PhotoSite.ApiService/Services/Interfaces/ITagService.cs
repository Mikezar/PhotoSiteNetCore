using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Interfaces
{
    public interface ITagService : IService
    {
        /// <summary>
        /// Get all extension tags
        /// </summary>
        /// <returns>All tags</returns>
        Task<ICollection<TagExtension>> GetExtAll();

        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns>All tags</returns>
        Task<ICollection<Tag>> GetAll();

        /// <summary>
        /// Update tag
        /// </summary>
        /// <param name="tag">Tag</param>
        Task Update(Tag tag);

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <returns>Identification new tag</returns>
        Task<IIdResult> Create(Tag tag);

        /// <summary>
        /// Delete tag
        /// </summary>
        /// <param name="id">Tag identification</param>
        /// <returns>Result</returns>
        Task Delete(int id);
    }
}