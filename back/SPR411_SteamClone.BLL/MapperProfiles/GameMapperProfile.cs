using AutoMapper;
using SPR411_SteamClone.BLL.Dtos.Game;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.BLL.MapperProfiles
{
    public class GameMapperProfile : Profile
    {
        public GameMapperProfile()
        {
            // CreateGameDto -> GameEntity
            CreateMap<CreateGameDto, GameEntity>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore());
        }
    }
}
