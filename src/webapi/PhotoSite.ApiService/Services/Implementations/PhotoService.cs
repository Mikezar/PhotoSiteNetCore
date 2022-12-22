using System.Threading.Tasks;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.ApiService.Services.Implementations
{
    /// <inheritdoc cref="IPhotoService"/>
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }


        public async Task<Photo?> Get(int id)
        {
            return await _photoRepository.Get(id);
        }
    }
}