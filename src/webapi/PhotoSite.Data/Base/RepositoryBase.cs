using System.Threading.Tasks;

namespace PhotoSite.Data.Base
{
    public abstract class RepositoryBase : IDbContext
    {
        protected readonly MainDbContext DbContext;

        /// <summary>
        /// ctor
        /// </summary>
        protected RepositoryBase(MainDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task SaveChanges()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}