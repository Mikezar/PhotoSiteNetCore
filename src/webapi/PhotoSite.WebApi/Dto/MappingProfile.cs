using AutoMapper;
using PhotoSite.Data.Entities;

namespace PhotoSite.WebApi.Dto
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Album, AlbumDto>();
        }
    }
}
