using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Entities;
using PhotoSite.WebApi.Common;
using PhotoSite.WebApi.Photo;

namespace PhotoSite.WebApi.Controllers.Admin
{
    /// <summary>
    /// Tags
    /// </summary>
    [Route("api/[controller]")]
    public class TagController : BaseController
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        // TODO: Add metod GetPopular

        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns>All extension tags</returns>
        [HttpGet("extall")]
        public async Task<List<TagExtensionDto>> GetExtAll()
        {
            var result = await _tagService.GetExtAll();
            return _mapper.Map<List<TagExtensionDto>>(result);
        }

        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns>All tags</returns>
        [HttpGet("all")]
        public async Task<List<TagDto>> GetAll()
        {
            var result = await _tagService.GetAll();
            return _mapper.Map<List<TagDto>>(result);
        }

        /// <summary>
        /// Update tag
        /// </summary>
        /// <param name="tag">Tag</param>
        [HttpPut]
        [Authorize]
        public async Task<ResultDto> Update(TagDto? tag)
        {
            if (tag is null)
                return new IdResultDto { ErrorMessage = "Tag cannot be empty" };
            var value = _mapper.Map<Tag>(tag);
            var result = await _tagService.Update(value);
            return _mapper.Map<ResultDto>(result);
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <returns>Identification new tag</returns>
        [HttpPost]
        [Authorize]
        public async Task<IdResultDto> Create(TagDto? tag)
        {
            if (tag is null)
                return new IdResultDto {ErrorMessage = "Tag cannot be empty"};
            var value = _mapper.Map<Tag>(tag);
            var result = await _tagService.Create(value);
            return _mapper.Map<IdResultDto>(result);
        }

        /// <summary>
        /// Delete tag
        /// </summary>
        /// <param name="id">Tag identification</param>
        /// <returns>Result</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ResultDto> Delete(int id)
        {
            var result = await _tagService.Delete(id);
            return _mapper.Map<ResultDto>(result);
        }
    }
}