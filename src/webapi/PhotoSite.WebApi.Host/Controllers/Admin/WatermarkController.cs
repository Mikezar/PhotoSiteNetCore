using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Core.ExtException;
using PhotoSite.Data.Entities;
using PhotoSite.WebApi.Photo;

namespace PhotoSite.WebApi.Controllers.Admin
{
    [Produces("application/json")]
    [Route("api/watermark")]
    [ApiController]
    [Authorize]
    public class WatermarkController : ControllerBase
    {
        private readonly IWatermarkService _watermarkService;
        private readonly IMapper _mapper;

        public WatermarkController(IWatermarkService watermarkService, IMapper mapper)
        {
            _watermarkService = watermarkService;
            _mapper = mapper;
        }

        [HttpGet("{photoId:int}")]
        public async Task<WatermarkDto?> GetByPhotoId(int photoId)
        {
            var result = await _watermarkService.GetByPhotoId(photoId);
            return _mapper.Map<WatermarkDto?>(result);
        }

        [HttpPut]
        public async Task Update(WatermarkDto watermark)
        {
            var value = _mapper.Map<Watermark>(watermark);
            await _watermarkService.Update(value);
        }

        [HttpPost]
        public async Task Create(WatermarkDto? watermark)
        {
            if (watermark is null)
                throw new UserException("Watermark cannot be empty");
            var value = _mapper.Map<Watermark>(watermark);
            await _watermarkService.Create(value);
        }
    }
}