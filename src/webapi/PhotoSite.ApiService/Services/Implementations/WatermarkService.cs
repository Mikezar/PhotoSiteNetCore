using System.Threading.Tasks;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Base;
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
        public async Task<IResult> Update(Watermark watermark)
        {
            var ext = await _watermarkRepository.Exists(watermark.PhotoId);
            if (!ext)
                return Result.GetError($"Do not this watermark attached to photo (photo's id={watermark.PhotoId})");
            await _watermarkRepository.Update(watermark);
            return Result.GetOk();
        }

        /// <inheritdoc cref="IWatermarkService.Create"/>
        public async Task<IResult> Create(Watermark watermark)
        {
            var ext = await _watermarkRepository.Exists(watermark.PhotoId);
            if (ext)
                return (IIdResult)Result.GetError($"Other watermark attached to photo (photo's id={watermark.PhotoId})");
            await _watermarkRepository.Create(watermark);
            return Result.GetOk();
        }

    }
}