using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.Data.Repositories.Interfaces
{
    public interface IAlbumRepository : ICrudRepository<Album, int>
    {
        Task<bool> Exists(int id);

        /// <summary>
        /// Check has child albums
        /// </summary>
        /// <param name="id">Album's identification</param>
        /// <returns>True - album has children, False - hasn't children</returns>
        Task<bool> ChildrenExists(int id);
    }
}