using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Core.ExtException;
using PhotoSite.WebApi.Album;
using PhotoSite.WebApi.Common;

namespace PhotoSite.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAlbumService _albumService;

        public AlbumController(
            IMapper mapper, 
            IAlbumService albumService)
        {
            _mapper = mapper;
            _albumService = albumService;
        }

        [HttpGet("{id:int}")]
        public async Task<AlbumDto?> Get(int id)
        {
            var result = await _albumService.GetChildren(id);
            return result is null ? null : _mapper.Map<AlbumDto>(result);
        }

        [HttpGet("{id:int}/children")]
        public async Task<AlbumDto[]?> GetChildren(int id)
        {
            var result = await _albumService.GetChildren(id);
            return result is null ? null : _mapper.Map<AlbumDto[]>(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IdResultDto> Create(AlbumDto? album)
        {
            if (album is null)
                throw new UserException("Album cannot be empty");
            var value = _mapper.Map<Data.Entities.Album>(album);
            var result = await _albumService.Create(value);
            return _mapper.Map<IdResultDto>(result);
        }

        [HttpPut]
        [Authorize]
        public async Task Update(AlbumDto? album)
        {
            if (album is null)
                throw new UserException("Album cannot be empty");
            var value = _mapper.Map<Data.Entities.Album>(album);
            await _albumService.Update(value);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task Delete(int id)
        {            
            await _albumService.Delete(id);
        }
    }
}
