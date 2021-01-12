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
    /// Watermarks
    /// </summary>
    [Authorize]
    [Route("api/wm")]
    public class WatermarkController : BaseController
    {
        private readonly IWatermarkService _watermarkService;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public WatermarkController(IWatermarkService watermarkService, IMapper mapper)
        {
            _watermarkService = watermarkService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get entity by photo's id
        /// </summary>
        /// <returns>All tags</returns>
        [HttpGet("byphoto/{photoId:int}")]
        public async Task<WatermarkDto?> GetByPhotoId(int photoId)
        {
            var result = await _watermarkService.GetByPhotoId(photoId);
            if (result == null)
                return null;
            return _mapper.Map<WatermarkDto>(result);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="watermark">Entity</param>
        [HttpPut]
        public async Task<ResultDto> Update(WatermarkDto watermark)
        {
            var value = _mapper.Map<Watermark>(watermark);
            var result = await _watermarkService.Update(value);
            return _mapper.Map<ResultDto>(result);
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="watermark">Entity</param>
        /// <returns>Identification new entity</returns>
        [HttpPost]
        public async Task<ResultDto> Create(WatermarkDto? watermark)
        {
            if (watermark is null)
                return new ResultDto { ErrorMessage = "Watermark cannot be empty" };
            var value = _mapper.Map<Watermark>(watermark);
            var result = await _watermarkService.Create(value);
            return _mapper.Map<ResultDto>(result);
        }
    }
}