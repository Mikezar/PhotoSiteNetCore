using System.Threading.Tasks;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.Data.Repositories.Interfaces
{
    public interface IBlackIpRepository : ICrudRepository<BlackIp, int>
    {
        Task<bool> Exists(string maskAddress);

        Task<bool> ExistsOtherBlackIpByMaskAddress(int id, string maskAddress);
    }
}