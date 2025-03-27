using HotelsApi.Dtos;
using HotelsApi.Entities;
using AutoMapper;

namespace HotelsApi.AutoMapperProfiles
{
    public class StateAutoMapperProfiles : Profile
    {
        public StateAutoMapperProfiles()
        {
            // Mapping from State entity to GetState DTO
            CreateMap<State, GetState>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.StateCode))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId));

            // Mapping from CreateState DTO to State entity
            CreateMap<CreateState, State>()
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.StateCode))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId));

            // Mapping from UpdateState DTO to State entity
            CreateMap<UpdateState, State>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.StateCode))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId));
        }
    }
}