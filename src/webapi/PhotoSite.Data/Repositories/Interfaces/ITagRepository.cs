using System.Threading.Tasks;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.Data.Repositories.Interfaces
{
    public interface ITagRepository : ICrudRepository<Tag, int>
    {
        Task<bool> ExistsByTagTitle(string tagTitle);

        Task<bool> ExistsOtherTagByTagTitle(int id, string? tagTitle);
    }
}