using System.Threading.Tasks;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.Data.Repositories.Interfaces
{
    public interface IWatermarkRepository : IRepository
    {
        Task Create(Watermark e, bool save = true);

        Task Update(Watermark e, bool save = true);

        Task<Watermark?> GetAsNoTrackingByPhotoId(int photoId);

        Task<bool> Exists(int photoId);
    }
}