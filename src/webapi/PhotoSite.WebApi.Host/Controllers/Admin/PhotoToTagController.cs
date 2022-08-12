using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.WebApi.Common;
using PhotoSite.WebApi.Photo;

namespace PhotoSite.WebApi.Controllers.Admin
{
    /// <summary>
    /// Photo's tags
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class PhotoToTagController : CustomControllerBase
    {
        private readonly IPhotoToTagService _photoToTagService;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public PhotoToTagController(IPhotoToTagService photoToTagService, IMapper mapper)
        {
            _photoToTagService = photoToTagService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get entities by photo's id
        /// </summary>
        /// <returns>All tags</returns>
        [HttpGet("byphoto/{photoId:int}")]
        public async Task<PhotoToTagDto[]> GetByPhotoId(int photoId)
        {
            var result = await _photoToTagService.GetByPhotoId(photoId);
            return _mapper.Map<PhotoToTagDto[]>(result);
        }

        /// <summary>
        /// Get tag's ids not in photo
        /// </summary>
        /// <returns>All tags</returns>
        [HttpGet("notbyphoto/{photoId:int}")]
        public async Task<IdResultDto[]> GetNotExistsInPhoto(int photoId)
        {
            var result = await _photoToTagService.GetNotExistsInPhoto(photoId);
            return result.Select(t => new IdResultDto() {Id = t}).ToArray();
        }

        /// <summary>
        /// Bind tags to photo
        /// </summary>
        [HttpPost("bindtagphoto")]
        public async Task BindTagsToPhoto(PhotoTagsDto photoTags)
        {
            var tagIds = photoTags.TagIds is null ? new int[0] : photoTags.TagIds.Select(t => t.Id).ToArray(); 
            await _photoToTagService.BindTagsToPhoto(photoTags.PhotoId, tagIds);
        }

    }
}