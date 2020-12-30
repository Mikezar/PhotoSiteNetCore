using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.Data.Repositories.Interfaces
{
    public interface IPhotoRepository : ICrudRepository<Photo, int>
    {
        
    }
}