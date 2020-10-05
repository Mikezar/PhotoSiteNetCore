using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;

namespace PhotoSite.WebApi.Controllers
{
    /// <summary>
    /// Photos
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : BaseController
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
    }
}