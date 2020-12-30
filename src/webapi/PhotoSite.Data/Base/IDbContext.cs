using System.Threading.Tasks;

namespace PhotoSite.Data.Base
{
    public interface IDbContext
    {
        Task SaveChanges();
    }
}