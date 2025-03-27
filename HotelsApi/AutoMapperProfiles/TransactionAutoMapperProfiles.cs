using HotelsApi.Dtos;
using HotelsApi.Entities;
using AutoMapper;

namespace HotelsApi.AutoMapperProfiles
{
    public class TransactionAutoMapperProfiles : Profile
    {
        public TransactionAutoMapperProfiles()
        {
            // Entity to DTO (Read operations)
            CreateMap<Transaction, GetTransaction>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.HotelName)) 
                .ForMember(dest => dest.DateFrom, opt => opt.MapFrom(src => src.DateFrom))
                .ForMember(dest => dest.DateTo, opt => opt.MapFrom(src => src.DateTo))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress));

            // DTO to Entity (Create operations)
            CreateMap<CreateTransaction, Transaction>()
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.HotelName))
                .ForMember(dest => dest.DateFrom, opt => opt.MapFrom(src => src.DateFrom))
                .ForMember(dest => dest.DateTo, opt => opt.MapFrom(src => src.DateTo))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress));

            // DTO to Entity (Update operations)
            CreateMap<UpdateTransaction, Transaction>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.HotelName))
                .ForMember(dest => dest.DateFrom, opt => opt.MapFrom(src => src.DateFrom))
                .ForMember(dest => dest.DateTo, opt => opt.MapFrom(src => src.DateTo))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress));
        }
    }
}