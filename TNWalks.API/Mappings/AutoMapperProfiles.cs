using AutoMapper;
using TNWalks.API.Models.Domain;
using TNWalks.API.Models.Dtos;

namespace TNWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // region mapping
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<CreateRegionDto, Region>();
            CreateMap<UpdateRegionDto, Region>();
            
            // walk mapping
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<CreateWalkDto, Walk>();
            CreateMap<UpdateWalkDto, Walk>();

            CreateMap<Difficulty, DifficultyDto>();
            
            // todos mapping
            CreateMap<Todo, TodoListDto>();
            CreateMap<Todo, TodoDetailDto>();
            CreateMap<CreateTodoDto, Todo>();
            CreateMap<UpdateTodoDto, Todo>();
        }
    }
}