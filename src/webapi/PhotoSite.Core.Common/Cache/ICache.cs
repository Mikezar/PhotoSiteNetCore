using System.Threading.Tasks;

namespace PhotoSite.Core.Cache
{
    public interface ICache
    {
        Task Remove();
    }
}