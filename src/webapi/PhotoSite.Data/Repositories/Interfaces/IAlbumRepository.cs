using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.Data.Repositories.Interfaces
{
    public interface IAlbumRepository : ICrudRepository<Album, int>
    {
        Task<ICollection<Album>> GetAsNoTrackingChild(int? parentId);
    }
}