using HotelsApi.Dtos;
using HotelsApi.Entities;
using AutoMapper;

namespace HotelsApi.AutoMapperProfiles
{
    public class HotelAutoMapperProfiles : Profile
    {
        public HotelAutoMapperProfiles()
        {
            // Mapping from Hotel entity to GetHotel DTO
            CreateMap<Hotel, GetHotel>()
                .ForMember(dest => dest.HotelCode, opt => opt.MapFrom(src => src.HotelCode))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.HotelName))
                .ForMember(dest => dest.HotelDescription, opt => opt.MapFrom(src => src.HotelDescription))
                .ForMember(dest => dest.BarangayId, opt => opt.MapFrom(src => src.BarangayId));

            // Mapping from CreateHotel DTO to Hotel entity
            CreateMap<CreateHotel, Hotel>()
                .ForMember(dest => dest.HotelCode, opt => opt.MapFrom(src => src.HotelCode))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.HotelName))
                .ForMember(dest => dest.HotelDescription, opt => opt.MapFrom(src => src.HotelDescription))
                .ForMember(dest => dest.BarangayId, opt => opt.MapFrom(src => src.BarangayId));

            // Mapping from UpdateHotel DTO to Hotel entity
            CreateMap<UpdateHotel, Hotel>()
                .ForMember(dest => dest.HotelCode, opt => opt.MapFrom(src => src.HotelCode))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.HotelName))
                .ForMember(dest => dest.HotelDescription, opt => opt.MapFrom(src => src.HotelDescription))
                .ForMember(dest => dest.BarangayId, opt => opt.MapFrom(src => src.BarangayId));
        }

       
    }
}