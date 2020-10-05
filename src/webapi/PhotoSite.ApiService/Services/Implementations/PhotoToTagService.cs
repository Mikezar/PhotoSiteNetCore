using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class PhotoToTagService : DbServiceBase, IPhotoToTagService
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="factory"></param>
        public PhotoToTagService(DataBaseFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Get entities by photo's id
        /// </summary>
        /// <returns>All tags</returns>
        public async Task<PhotoToTag[]> GetByPhotoId(int photoId)
        {
            var context = DbFactory.GetReadContext();
            return await context.PhotoToTags.Where(t => t.PhotoId == photoId).ToArrayAsync();
        }

        /// <summary>
        /// Get tag's ids not in photo
        /// </summary>
        /// <returns>All tags</returns>
        public async Task<int[]> GetNotExistsInPhoto(int photoId)
        {
            var context = DbFactory.GetReadContext();
            var result = from t in context.Tags
                join pt in context.PhotoToTags on t.Id equals pt.TagId into g
                from et in g.DefaultIfEmpty() 
                where et == null
                select t.Id;
            return await result.ToArrayAsync();
        }

        /// <summary>
        /// Bind tags to photo
        /// </summary>
        public async Task<Result> BindTagsToPhoto(int photoId, int[] tagIds)
        {
            await using var context = DbFactory.GetWriteContext();
            var existsTagIds = await context.PhotoToTags.Where(t => t.PhotoId == photoId).ToArrayAsync();

            // Remove deleted
            foreach (var value in existsTagIds)
            {
                var exists = tagIds.Any(t => t == value.TagId);
                if (!exists)
                    context.Remove(value);
            }

            // Add new
            foreach (var tagId in tagIds)
            {
                var exists = existsTagIds.Any(t => t.TagId == tagId);
                if (!exists)
                    await context.AddAsync(new PhotoToTag() {PhotoId = photoId, TagId = tagId});
            }

            await context.SaveChangesAsync();

            return Result.GetOk();
        }
    }
}