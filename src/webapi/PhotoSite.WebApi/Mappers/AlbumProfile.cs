using AutoMapper;
using PhotoSite.Data.Entities;
using PhotoSite.Dto.Album;

namespace PhotoSite.WebApi.Mappers
{
    internal class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<Album, AlbumDto>();
            CreateMap<Album, AlbumSimpleDto>();
        }
    }
}
