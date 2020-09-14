using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;

namespace PhotoSite.WebApi.Controllers
{
    /// <summary>
    /// Gallery
    /// </summary>
    [Produces("application/json")]
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
    }
}
