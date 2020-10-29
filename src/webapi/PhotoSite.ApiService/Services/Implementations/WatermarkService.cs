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
            return await DbContext.Watermarks.FirstOrDefaultAsync(t => t.PhotoId == photoId);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="watermark">Entity</param>
        public async Task<Result> Update(Watermark watermark)
        {
            //await using var context = DbFactory.GetWriteContext();
            var value = await DbContext.Watermarks.FirstOrDefaultAsync(t => t.Id == watermark.Id);
            if (value == null)
                return Result.GetError($"Not found watermark id={watermark.Id}");

            var ext = await DbContext.Watermarks.AnyAsync(t => t.PhotoId == watermark.PhotoId && t.Id != watermark.Id);
            if (ext)
                return Result.GetError($"Other watermark attached to photo (photo's id={watermark.PhotoId})");

            DbContext.Update(watermark);
            DbContext.Attach(watermark);
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
            //var context = DbFactory.GetWriteContext();
            var ext = await DbContext.Watermarks.AnyAsync(t => t.PhotoId == watermark.PhotoId);
            if (ext)
                return IdResult.GetError($"Other watermark attached to photo (photo's id={watermark.PhotoId})");

            var maxId = 0;
            if (await DbContext.Watermarks.CountAsync() > 0)
                maxId = await DbContext.Watermarks.MaxAsync(t => t.Id);
            maxId += 1;

            watermark.Id = maxId;
            await DbContext.AddAsync(watermark);
            //DbContext.Attach(watermark);
            await DbContext.SaveChangesAsync();

            return IdResult.GetOk(maxId);
        }

    }
}