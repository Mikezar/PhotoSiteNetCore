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

        public async Task<bool> Exists(int id)
        {
            return await DbContext.Albums!.AsNoTracking().AnyAsync(t => t.Id == id);
        }

        /// <inheritdoc cref="IAlbumRepository.ChildrenExists"/>
        public async Task<bool> ChildrenExists(int id)
        {
            return await DbContext.Albums!.AsNoTracking().AnyAsync(t => t.ParentId == id);
        }
    }
}