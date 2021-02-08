using PhotoSite.ManagementBoard.Models;
using PhotoSite.ManagementBoard.Services.Abstract;
using PhotoSite.WebApi.Photo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Implementation
{
    internal sealed class TagService : ITagService
    {
        private static string TagEndpoint = "api/tag";
        private static string GetAllTagEndpoint = $"{TagEndpoint}/extall";

        private readonly IHttpHandler _httpHandler;

        public TagService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task<ResultWrapper<IList<TagExtensionDto>>> GetAllTagsAsync()
        {
            return await _httpHandler.GetAsync<IList<TagExtensionDto>>(GetAllTagEndpoint);
        }

        public async Task<NoResultWrapper> AddTagAsync(TagDto tagDto)
        {
            return await _httpHandler.PostAsync(TagEndpoint, tagDto);
        }

        public async Task<NoResultWrapper> UpdateTagAsync(TagDto tagDto)
        {
            return await _httpHandler.PutAsync(TagEndpoint, tagDto);
        }

        public Task<NoResultWrapper> DeleteTagAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
