using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class WatermarkService : DbServiceBase, IWatermarkService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public WatermarkService(MainDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get entity by photo's id
        /// </summary>
        /// <returns>All tags</returns>
        public async Task<Watermark?> GetByPhotoId(int photoId)
        {
            return await DbContext.Watermarks.AsNoTracking().FirstOrDefaultAsync(t => t.PhotoId == photoId);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="watermark">Entity</param>
        public async Task<Result> Update(Watermark watermark)
        {
            var ext = await DbContext.Watermarks.AsNoTracking().AnyAsync(t => t.PhotoId == watermark.PhotoId);
            if (!ext)
                return Result.GetError($"Do not this watermark attached to photo (photo's id={watermark.PhotoId})");

            DbContext.Attach(watermark);
            DbContext.Update(watermark);
            await DbContext.SaveChangesAsync();

            return Result.GetOk();
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="watermark">Entity</param>
        /// <returns>Identification new entity</returns>
        public async Task<IdResult> Create(Watermark watermark)
        {
            var ext = await DbContext.Watermarks.AsNoTracking().AnyAsync(t => t.PhotoId == watermark.PhotoId);
            if (ext)
                return IdResult.GetError($"Other watermark attached to photo (photo's id={watermark.PhotoId})");

            await DbContext.AddAsync(watermark);
            await DbContext.SaveChangesAsync();

            return IdResult.GetOk(watermark.PhotoId);
        }

    }
}