using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.WebApi.Photo;

namespace PhotoSite.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/photos")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public PhotoController(
            IMapper mapper,
            IPhotoService photoService)
        {
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet("{id:int}")]
        public async Task<PhotoDto?> Get(int id)
        {
            var result = await _photoService.Get(id);
            return _mapper.Map<PhotoDto?>(result);
        }
    }
}