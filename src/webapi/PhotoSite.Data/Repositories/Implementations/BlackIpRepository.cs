using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.Data.Repositories.Implementations
{
    public class BlackIpRepository : CrudRepositoryBase<BlackIp, int>, IBlackIpRepository
    {
        public BlackIpRepository(MainDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> Exists(string maskAddress)
        {
            return await DbContext.BlackIps!.AsNoTracking().AnyAsync(t => t.MaskAddress == maskAddress);
        }

        public async Task<bool> ExistsOtherBlackIpByMaskAddress(int id, string maskAddress)
        {
            return await DbContext.BlackIps!.AsNoTracking().AnyAsync(t => t.MaskAddress == maskAddress && t.Id != id);
        }
    }
}