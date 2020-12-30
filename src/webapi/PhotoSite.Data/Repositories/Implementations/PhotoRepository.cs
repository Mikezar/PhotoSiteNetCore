using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.Data.Repositories.Implementations
{
    public class PhotoRepository : CrudRepositoryBase<Photo, int>, IPhotoRepository
    {
        public PhotoRepository(MainDbContext dbContext) : base(dbContext)
        {
        }

    }
}