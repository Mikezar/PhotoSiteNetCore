using AutoMapper;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Data.Admin;
using PhotoSite.WebApi.Admin;

namespace PhotoSite.WebApi.Mappers
{
    internal class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<LoginState, LoginStateDto>();
            CreateMap<LoginStatus, LoginStatusDto>();
            CreateMap<Settings, SettingsDto>();
        }
    }
}