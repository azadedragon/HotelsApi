using HotelsApi.Dtos;
using HotelsApi.Entities;
using AutoMapper;

namespace HotelsApi.AutoMapperProfiles
{
    public class CityAutoMapperProfiles : Profile
    {
        public CityAutoMapperProfiles()
        {
            // Mapping from City entity to GetCity DTO
            CreateMap<City, GetCity>()
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
                .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.CityCode))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId));

            // Mapping from CreateCity DTO to City entity
            CreateMap<CreateCity, City>()
                .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.CityCode))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId));

            // Mapping from UpdateCity DTO to City entity
            CreateMap<UpdateCity, City>()
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
                .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.CityCode))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId));
        }
    }
}