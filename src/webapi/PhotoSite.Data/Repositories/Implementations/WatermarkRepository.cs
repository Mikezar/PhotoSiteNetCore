using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.Data.Repositories.Implementations
{
    public class WatermarkRepository : RepositoryBase, IWatermarkRepository
    {
        public WatermarkRepository(MainDbContext dbContext) : base(dbContext)
        {

        }

        public async Task Create(Watermark e, bool save = true)
        {
            await DbContext.AddAsync(e);
            if (save)
                await DbContext.SaveChangesAsync();
        }

        public async Task Update(Watermark e, bool save = true)
        {
            DbContext.Update(e);
            if (save)
                await DbContext.SaveChangesAsync();
        }

        public async Task<Watermark?> GetAsNoTrackingByPhotoId(int photoId)
        {
            return await DbContext.Watermarks!.AsNoTracking().FirstOrDefaultAsync(t => t.PhotoId == photoId);
        }

        public async Task<bool> Exists(int photoId)
        {
            return await DbContext.Watermarks!.AsNoTracking().AnyAsync(t => t.PhotoId == photoId);
        }

    }
}