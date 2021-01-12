using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Interfaces
{
    public interface IAlbumService : IService
    {
        /// <summary>
        /// Get album by identification
        /// </summary>
        /// <param name="id">Album's identification</param>
        /// <returns>Album</returns>
        Task<Album?> Get(int id);

        /// <summary>
        /// Get child albums
        /// </summary>
        /// <param name="id">Parent album's identification</param>
        /// <returns>Albums</returns>
        Task<ICollection<Album>?> GetChildren(int id);

        /// <summary>
        /// Create new album
        /// </summary>
        /// <param name="album">Album</param>
        /// <returns>Identification of album</returns>
        Task<IIdResult> Create(Album album);

        /// <summary>
        /// Update album
        /// </summary>
        /// <param name="album">Album</param>
        /// <returns>Result</returns>
        Task<IResult> Update(Album album);

        /// <summary>
        /// Delete album
        /// </summary>
        Task<IResult> Delete(int id);
    }
}