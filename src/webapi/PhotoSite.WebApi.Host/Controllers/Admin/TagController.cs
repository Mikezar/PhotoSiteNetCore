﻿using System.Threading.Tasks;
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
    [Produces("application/json")]
    [Route("api/tags")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
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

        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns>All tags</returns>
        [HttpGet("getall")]
        public async Task<TagDto[]> GetAll()
        {
            var result = await _tagService.GetAll();
            return _mapper.Map<TagDto[]>(result);
        }

        /// <summary>
        /// Update tag
        /// </summary>
        /// <param name="tag">Tag</param>
        [HttpPost("update")]
        public async Task<ResultDto> Update(TagDto tag)
        {
            var t = _mapper.Map<Tag>(tag);
            var result = await _tagService.Update(t);
            return _mapper.Map<ResultDto>(result);
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="tagTitle">Tag's title</param>
        /// <returns>Identification new tag</returns>
        [HttpPost("create")]
        public async Task<IdResultDto> Create(TagTitleDto tagTitle)
        {
            if (tagTitle == null)
                return new IdResultDto {ErrorMessage = "Tag cannot be empty"};
            var result = await _tagService.Create(tagTitle.Title!);
            return _mapper.Map<IdResultDto>(result);
        }
    }
}