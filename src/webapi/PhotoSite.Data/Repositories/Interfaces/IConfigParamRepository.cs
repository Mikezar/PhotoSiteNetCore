using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.Data.Repositories.Interfaces
{
    public interface IConfigParamRepository : IRepository
    {
        Task Create(ConfigParam e, bool save = true);

        Task<ConfigParam?> Get(string name);

        Task<ICollection<ConfigParam>> GetAsNoTrackingAll();
    }
}