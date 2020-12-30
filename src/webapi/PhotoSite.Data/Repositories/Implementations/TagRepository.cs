using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.Data.Repositories.Implementations
{
    public class TagRepository : CrudRepositoryBase<Tag, int>, ITagRepository
    {
        public TagRepository(MainDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsByTagTitle(string tagTitle)
        {
            return await DbContext.Tags!.AsNoTracking().AnyAsync(t => t.Title == tagTitle);
        }

        public async Task<bool> ExistsOtherTagByTagTitle(int id, string? tagTitle)
        {
            return await DbContext.Tags!.AsNoTracking().AnyAsync(t => t.Title == tagTitle && t.Id != id);
        }
    }
}