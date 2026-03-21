using AutoMapper;
using SPR411_SteamClone.BLL.Dtos.Developer;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.BLL.MapperProfiles
{
    public class DeveloperMapperProfile : Profile
    {
        public DeveloperMapperProfile()
        {
            // DeveloperEntity -> DeveloperDto
            CreateMap<DeveloperEntity, DeveloperDto>();

            // CreateDeveloperDto -> DeveloperEntity
            CreateMap<CreateDeveloperDto, DeveloperEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // UpdateDeveloperDto -> DeveloperEntity
            CreateMap<UpdateDeveloperDto, DeveloperEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
