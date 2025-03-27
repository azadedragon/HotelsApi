using HotelsApi.Dtos;
using HotelsApi.Entities;
using AutoMapper;

namespace HotelsApi.AutoMapperProfiles
{
    public class BarangayAutoMapperProfiles : Profile
    {
        public BarangayAutoMapperProfiles()
        {
            // Mapping from Barangay entity to GetBarangay DTO
            CreateMap<Barangay, GetBarangay>()
                .ForMember(dest => dest.BarangayId, opt => opt.MapFrom(src => src.BarangayId))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.BarangayName, opt => opt.MapFrom(src => src.BarangayName))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId));

            // Mapping from CreateBarangay DTO to Barangay entity
            CreateMap<CreateBarangay, Barangay>()
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.BarangayName, opt => opt.MapFrom(src => src.BarangayName))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId));

            // Mapping from UpdateBarangay DTO to Barangay entity
            CreateMap<UpdateBarangay, Barangay>()
                .ForMember(dest => dest.BarangayId, opt => opt.MapFrom(src => src.BarangayId))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.BarangayName, opt => opt.MapFrom(src => src.BarangayName))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId));
        }
    }
}