using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Base;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class PhotoService : DbServiceBase, IPhotoService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PhotoService(MainDbContext dbContext) : base(dbContext)
        {
        }


    }
}