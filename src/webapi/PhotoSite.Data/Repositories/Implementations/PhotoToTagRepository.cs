using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.Data.Repositories.Implementations
{
    public class PhotoToTagRepository : RepositoryBase, IPhotoToTagRepository
    {
        public PhotoToTagRepository(MainDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<PhotoToTag>> GetAll()
        {
            return await DbContext.PhotoToTags!.AsNoTracking().ToArrayAsync();
        }

        public async Task UnBindTag(int tagId, bool save = true)
        {
            var tags = await DbContext.PhotoToTags!.Where(t => t.TagId == tagId).ToArrayAsync();
            DbContext.PhotoToTags!.RemoveRange(tags);
            if (save)
                await DbContext.SaveChangesAsync();
        }
    }
}