using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.Data.Repositories.Implementations
{
    public class ConfigParamRepository : RepositoryBase, IConfigParamRepository
    {
        public ConfigParamRepository(MainDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Create(ConfigParam e, bool save = true)
        {
            await DbContext.AddAsync(e);
            if (save)
                await DbContext.SaveChangesAsync();
        }

        public async Task<ConfigParam?> Get(string name)
        {
            return await DbContext.ConfigParam!.FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task<ICollection<ConfigParam>> GetAsNoTrackingAll()
        {
            return await DbContext.ConfigParam!.AsNoTracking().ToArrayAsync();
        }
    }
}