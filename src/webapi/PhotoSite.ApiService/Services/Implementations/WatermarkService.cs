using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class WatermarkService : IWatermarkService
    {
        private readonly DbFactory _factory;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="factory"></param>
        public WatermarkService(DbFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Get entity by photo's id
        /// </summary>
        /// <returns>All tags</returns>
        public async Task<Watermark?> GetByPhotoId(int photoId)
        {
            var context = _factory.GetReadContext();
            return await context.Watermarks.FirstOrDefaultAsync(t => t.PhotoId == photoId);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="watermark">Entity</param>
        public async Task<Result> Update(Watermark watermark)
        {
            await using var context = _factory.GetWriteContext();
            var value = await context.Watermarks.FirstOrDefaultAsync(t => t.Id == watermark.Id);
            if (value == null)
                return Result.GetError($"Not found watermark id={watermark.Id}");

            var ext = await context.Watermarks.AnyAsync(t => t.PhotoId == watermark.PhotoId && t.Id != watermark.Id);
            if (ext)
                return Result.GetError($"Other watermark attached to photo (photo's id={watermark.PhotoId})");

            context.Attach(watermark);
            await context.SaveChangesAsync();

            return Result.GetOk();
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="watermark">Entity</param>
        /// <returns>Identification new entity</returns>
        public async Task<IdResult> Create(Watermark watermark)
        {
            await using var context = _factory.GetWriteContext();
            var ext = await context.Watermarks.AnyAsync(t => t.PhotoId == watermark.PhotoId);
            if (ext)
                return IdResult.GetError($"Other watermark attached to photo (photo's id={watermark.PhotoId})");

            var maxId = await context.Watermarks.MaxAsync(t => t.Id);
            maxId += 1;

            context.Attach(watermark);
            watermark.Id = maxId;
            await context.SaveChangesAsync();

            return IdResult.GetOk(maxId);
        }

    }
}