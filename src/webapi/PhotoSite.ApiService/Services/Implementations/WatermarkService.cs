using System.Threading.Tasks;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Core.ExtException;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.ApiService.Services.Implementations
{
    /// <inheritdoc cref="IWatermarkService"/>
    public class WatermarkService : IWatermarkService
    {
        private readonly IWatermarkRepository _watermarkRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public WatermarkService(IWatermarkRepository watermarkRepository)
        {
            _watermarkRepository = watermarkRepository;
        }

        /// <inheritdoc cref="IWatermarkService.GetByPhotoId"/>
        public async Task<Watermark?> GetByPhotoId(int photoId)
        {
            return await _watermarkRepository.GetAsNoTrackingByPhotoId(photoId);
        }

        /// <inheritdoc cref="IWatermarkService.Update"/>
        public async Task Update(Watermark watermark)
        {
            if (!await _watermarkRepository.Exists(watermark.PhotoId))
                throw new UserException($"Do not this watermark attached to photo (photo's id={watermark.PhotoId})");
            await _watermarkRepository.Update(watermark);
        }

        /// <inheritdoc cref="IWatermarkService.Create"/>
        public async Task Create(Watermark watermark)
        {
            if (await _watermarkRepository.Exists(watermark.PhotoId))
                throw new UserException($"Other watermark attached to photo (photo's id={watermark.PhotoId})");
            await _watermarkRepository.Create(watermark);
        }

    }
}