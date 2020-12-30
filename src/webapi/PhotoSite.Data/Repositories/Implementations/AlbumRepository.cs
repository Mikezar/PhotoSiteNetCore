using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.Data.Repositories.Implementations
{
    public class AlbumRepository : CrudRepositoryBase<Album, int>, IAlbumRepository
    {
        public AlbumRepository(MainDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<Album>> GetAsNoTrackingChild(int? parentId)
        {
            return await DbContext.Albums!.AsNoTracking().Where(t => t.ParentId == parentId).ToArrayAsync();
        }
    }
}