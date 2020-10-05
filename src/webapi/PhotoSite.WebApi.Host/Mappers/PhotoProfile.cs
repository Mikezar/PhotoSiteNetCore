using AutoMapper;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.WebApi.Common;
using PhotoSite.WebApi.Photo;

namespace PhotoSite.WebApi.Mappers
{
    internal class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Data.Entities.PhotoToTag, PhotoToTagDto>().ReverseMap();
            CreateMap<Data.Entities.Watermark, WatermarkDto>().ReverseMap();
            CreateMap<Data.Entities.Tag, TagDto>().ReverseMap();
            CreateMap<IdResult, IdResultDto>();
            CreateMap<Result, ResultDto>();

        }   
    }
}