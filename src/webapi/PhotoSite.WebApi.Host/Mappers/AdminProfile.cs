using AutoMapper;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Data.Admin;
using PhotoSite.Data.Entities;
using PhotoSite.WebApi.Admin;
using PhotoSite.WebApi.Admin.Authorize;

namespace PhotoSite.WebApi.Mappers
{
    internal class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<LoginState, LoginStateDto>();
            CreateMap<LoginStatus, LoginStatusDto>();
            CreateMap<Settings, ConfigParamDto>().ReverseMap();
            CreateMap<BlackIp, BlackIpDto>().ReverseMap();
        }
    }
}