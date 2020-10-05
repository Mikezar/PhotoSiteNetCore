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
        /// <param name="factory"></param>
        public PhotoService(DataBaseFactory factory) : base(factory)
        {
        }


    }
}