using PhotoSite.Data.Base;

namespace PhotoSite.ApiService.Base
{
    public class DbServiceBase
    {
        protected readonly DataBaseFactory DbFactory;

        /// <summary>
        /// ctor
        /// </summary>
        public DbServiceBase(DataBaseFactory dbFactory)
        {
            DbFactory = dbFactory;
        }
    }
}