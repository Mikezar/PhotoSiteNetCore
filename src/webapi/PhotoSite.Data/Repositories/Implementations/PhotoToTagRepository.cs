using System.Collections.Generic;
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
    }
}