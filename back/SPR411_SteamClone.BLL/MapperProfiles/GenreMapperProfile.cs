using AutoMapper;
using SPR411_SteamClone.BLL.Dtos.Genre;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.BLL.MapperProfiles
{
    public class GenreMapperProfile : Profile
    {
        public GenreMapperProfile()
        {
            // GenreEntity -> GenreDto
            CreateMap<GenreEntity, GenreDto>();

            // CreateGenreDto -> GenreEntity
            CreateMap<CreateGenreDto, GenreEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // UpdateGenreDto -> GenreEntity
            CreateMap<UpdateGenreDto, GenreEntity>();
        }
    }
}
