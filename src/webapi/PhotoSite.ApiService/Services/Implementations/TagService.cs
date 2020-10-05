using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class TagService : DbServiceBase, ITagService
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="factory"></param>
        public TagService(DataBaseFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns>All tags</returns>
        public async Task<Tag[]> GetAll()
        {
            var context = DbFactory.GetReadContext();
            return await context.Tags.ToArrayAsync();
        }

        /// <summary>
        /// Update tag
        /// </summary>
        /// <param name="tag">Tag</param>
        public async Task<Result> Update(Tag tag)
        {
            await using var context = DbFactory.GetWriteContext();
            var value = await context.Tags.FirstOrDefaultAsync(t => t.Id == tag.Id);
            if (value == null)
                return Result.GetError($"Not found tag id={tag.Id}");

            var ext = await context.Tags.AnyAsync(t => t.Title == tag.Title && t.Id != tag.Id);
            if (ext)
                return Result.GetError($"Tag's title '{tag.Title}' exists in other tag");

            value.Title = tag.Title;
            await context.SaveChangesAsync();

            return Result.GetOk();
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="tagTitle">Tag's title</param>
        /// <returns>Identification new tag</returns>
        public async Task<IdResult> Create(string tagTitle)
        {
            await using var context = DbFactory.GetWriteContext();
            var ext = await context.Tags.AnyAsync(t => t.Title == tagTitle);
            if (ext)
                return IdResult.GetError($"Tag's title '{tagTitle}' exists in other tag");

            var maxId = await context.Tags.MaxAsync(t => t.Id);
            maxId += 1;

            await context.AddAsync(new Tag {Id = maxId, Title = tagTitle});
            await context.SaveChangesAsync();

            return IdResult.GetOk(maxId);
        }
    }
}