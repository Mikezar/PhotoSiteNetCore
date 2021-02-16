using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.Data.Repositories.Interfaces
{
    public interface IPhotoToTagRepository : IRepository
    {
        Task<ICollection<PhotoToTag>> GetAll();

        Task UnBindTag(int tagId, bool save = true);
    }
}