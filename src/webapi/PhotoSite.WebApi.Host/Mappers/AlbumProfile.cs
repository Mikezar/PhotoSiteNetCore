using AutoMapper;
using PhotoSite.WebApi.Album;

namespace PhotoSite.WebApi.Mappers
{
    internal class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<Data.Entities.Album, AlbumDto>().ReverseMap();
            CreateMap<Data.Entities.Album, AlbumSimpleDto>();
        }
    }
}
