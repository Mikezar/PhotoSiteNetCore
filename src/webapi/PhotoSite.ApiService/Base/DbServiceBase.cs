using PhotoSite.Data.Base;

namespace PhotoSite.ApiService.Base
{
    public class DbServiceBase
    {
        protected readonly MainDbContext DbContext;

        /// <summary>
        /// ctor
        /// </summary>
        public DbServiceBase(MainDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}