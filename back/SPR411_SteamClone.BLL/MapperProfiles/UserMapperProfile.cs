using AutoMapper;
using SPR411_SteamClone.BLL.Dtos.Auth;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.BLL.MapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<RegisterDto, UserEntity>();
        }
    }
}
