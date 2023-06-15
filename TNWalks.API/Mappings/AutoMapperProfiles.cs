using AutoMapper;
using TNWalks.API.Models.Domain;
using TNWalks.API.Models.Dtos;

namespace TNWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<CreateRegionDto, Region>();
            CreateMap<UpdateRegionDto, Region>();
        }
    }
}