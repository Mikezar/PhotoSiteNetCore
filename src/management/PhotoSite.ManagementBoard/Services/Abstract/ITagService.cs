using PhotoSite.ManagementBoard.Models;
using PhotoSite.WebApi.Photo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Abstract
{
    public interface ITagService : IService
    {
        Task<ResultWrapper<IList<TagExtensionDto>>> GetAllTagsAsync();
        Task<NoResultWrapper> AddTagAsync(TagDto tagDto);
        Task<NoResultWrapper> UpdateTagAsync(TagDto tagDto);
        Task<NoResultWrapper> DeleteTagAsync();
    }
}
