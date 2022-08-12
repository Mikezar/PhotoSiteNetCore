using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.WebApi.Photo;

namespace PhotoSite.WebApi.Controllers
{
    /// <summary>
    /// Photos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : CustomControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        /// <summary>
        /// ctor
        /// </summary>
        public PhotoController(
            IMapper mapper,
            IPhotoService photoService)
        {
            _mapper = mapper;
            _photoService = photoService;
        }

        /// <summary>
        /// Get entity by photo's id
        /// </summary>
        /// <returns>All tags</returns>
        [HttpGet("get")]
        public async Task<PhotoDto?> Get([FromQuery] int id)
        {
            var result = await _photoService.Get(id);
            return _mapper.Map<PhotoDto?>(result);
        }
    }
}