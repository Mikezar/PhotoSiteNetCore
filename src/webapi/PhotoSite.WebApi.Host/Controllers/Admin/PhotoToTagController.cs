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
    [Produces("application/json")]
    [Route("api/photoToTag")]
    [ApiController]
    [Authorize]
    public class PhotoToTagController : ControllerBase
    {
        private readonly IPhotoToTagService _photoToTagService;
        private readonly IMapper _mapper;

        public PhotoToTagController(IPhotoToTagService photoToTagService, IMapper mapper)
        {
            _photoToTagService = photoToTagService;
            _mapper = mapper;
        }

        [HttpGet("byphoto/{photoId:int}")]
        public async Task<PhotoToTagDto[]> GetByPhotoId(int photoId)
        {
            var result = await _photoToTagService.GetByPhotoId(photoId);
            return _mapper.Map<PhotoToTagDto[]>(result);
        }

        [HttpGet("notbyphoto/{photoId:int}")]
        public async Task<IdResultDto[]> GetNotExistsInPhoto(int photoId)
        {
            var result = await _photoToTagService.GetNotExistsInPhoto(photoId);
            return result.Select(t => new IdResultDto() {Id = t}).ToArray();
        }

        [HttpPost("bindtagphoto")]
        public async Task BindTagsToPhoto(PhotoTagsDto photoTags)
        {
            var tagIds = photoTags.TagIds is null ? new int[0] : photoTags.TagIds.Select(t => t.Id).ToArray(); 
            await _photoToTagService.BindTagsToPhoto(photoTags.PhotoId, tagIds);
        }

    }
}