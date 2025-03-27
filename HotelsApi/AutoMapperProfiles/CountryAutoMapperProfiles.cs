using HotelsApi.Dtos;
using HotelsApi.Entities;
using AutoMapper;

namespace HotelsApi.AutoMapperProfiles
{
    public class CountryAutoMapperProfiles : Profile
    {
        public CountryAutoMapperProfiles()
        {
            // Mapping from Country entity to GetCountry DTO
            CreateMap<Country, GetCountry>()
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.CountryCode))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.CountryName));

            // Mapping from CreateCountry DTO to Country entity
            CreateMap<CreateCountry, Country>()
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.CountryCode))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.CountryName));

            // Mapping from UpdateCountry DTO to Country entity
            CreateMap<UpdateCountry, Country>()
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.CountryCode))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.CountryName));
        }
    }
}