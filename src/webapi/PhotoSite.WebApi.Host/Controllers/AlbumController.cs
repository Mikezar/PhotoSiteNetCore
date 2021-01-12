using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.WebApi.Album;
using PhotoSite.WebApi.Common;

namespace PhotoSite.WebApi.Controllers
{
    /// <summary>
    /// Gallery
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IAlbumService _albumService;

        /// <summary>
        /// ctor
        /// </summary>
        public AlbumController(
            IMapper mapper, 
            IAlbumService albumService)
        {
            _mapper = mapper;
            _albumService = albumService;
        }

        /// <summary>
        /// Get album by identification
        /// </summary>
        /// <param name="id">Album's identification</param>
        /// <returns>Album</returns>
        [HttpGet("get/{id:int}")]
        public async Task<AlbumDto?> Get(int id)
        {
            var result = await _albumService.GetChildren(id);
            return result is null ? null : _mapper.Map<AlbumDto>(result);
        }

        /// <summary>
        /// Get child albums
        /// </summary>
        /// <param name="id">Parent album's identification (For get root albums sent 0)</param>
        /// <returns>Albums</returns>
        [HttpGet("getchildren/{id:int}")]
        public async Task<AlbumDto[]?> GetChildren(int id)
        {
            var result = await _albumService.GetChildren(id);
            return result is null ? null : _mapper.Map<AlbumDto[]>(result);
        }

        /// <summary>
        /// Create new album
        /// </summary>
        /// <param name="album">Album</param>
        /// <returns>Identification of album</returns>
        [HttpPost]
        [Authorize]
        public async Task<IdResultDto> Create(AlbumDto? album)
        {
            if (album is null)
                return new IdResultDto { ErrorMessage = "Album cannot be empty" };
            var value = _mapper.Map<Data.Entities.Album>(album);
            var result = await _albumService.Create(value);
            return _mapper.Map<IdResultDto>(result);
        }

        /// <summary>
        /// Update album
        /// </summary>
        /// <param name="album">Album</param>
        /// <returns>Result</returns>
        [HttpPut]
        [Authorize]
        public async Task<ResultDto> Update(AlbumDto? album)
        {
            if (album is null)
                return new IdResultDto { ErrorMessage = "Album cannot be empty" };
            var value = _mapper.Map<Data.Entities.Album>(album);
            var result = await _albumService.Update(value);
            return _mapper.Map<ResultDto>(result);
        }

        /// <summary>
        /// Delete album
        /// </summary>
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ResultDto> Delete(int id)
        {            
            var result = await _albumService.Delete(id);
            return _mapper.Map<ResultDto>(result);
        }
    }
}
