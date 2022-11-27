using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Core.ExtException;
using PhotoSite.Data.Entities;
using PhotoSite.WebApi.Common;
using PhotoSite.WebApi.Photo;

namespace PhotoSite.WebApi.Controllers.Admin
{
    [Produces("application/json")]
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }


        [HttpGet("ext-all")]
        public async Task<List<TagExtensionDto>> GetExtAll()
        {
            var result = await _tagService.GetExtAll();
            return _mapper.Map<List<TagExtensionDto>>(result);
        }

        [HttpGet("all")]
        public async Task<List<TagDto>> GetAll()
        {
            var result = await _tagService.GetAll();
            return _mapper.Map<List<TagDto>>(result);
        }

        [HttpPut]
        [Authorize]
        public async Task Update(TagDto? tag)
        {
            if (tag is null)
                throw new UserException("Tag cannot be empty");
            var value = _mapper.Map<Tag>(tag);
            await _tagService.Update(value);
        }

        [HttpPost]
        [Authorize]
        public async Task<IdResultDto> Create(TagDto? tag)
        {
            if (tag is null)
                throw new UserException("Tag cannot be empty");
            var value = _mapper.Map<Tag>(tag);
            var result = await _tagService.Create(value);
            return _mapper.Map<IdResultDto>(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task Delete(int id)
        {
            await _tagService.Delete(id);
        }
    }
}