using AutoMapper;
using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using HotelsApi.Repositories;
using HotelsApi.Validator;
using FluentValidation.Results;
using FluentValidation;

namespace HotelsApi.Services
{
    public interface IHotelService
    {
        Task<List<GetHotel>> GetAllHotels();
        Task<GetHotel?> GetHotelById(int id);
        Task<GetHotel> CreateHotel(CreateHotel hotel);
        Task<GetHotel?> UpdateHotel(int id, UpdateHotel hotel);
        Task<bool> DeleteHotel(int id);
    }
    public class HotelServices : IHotelService
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IValidator<CreateHotel> CreateHotelValidator;
        private readonly IValidator<UpdateHotel> updateHotelValidator;
        private readonly IMapper mapper;
        public HotelServices(IHotelRepository hotelRepository, IMapper mapper, IValidator<CreateHotel> CreateHotelValidator, IValidator<UpdateHotel> updateHotelValidator)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
            this.CreateHotelValidator = CreateHotelValidator;
            this.updateHotelValidator = updateHotelValidator;
        }
        public async Task<List<GetHotel>> GetAllHotels()
        {
            var hotel = await hotelRepository.GetAllHotels();

            return mapper.Map<List<GetHotel>>(hotel);
        }

        public async Task<GetHotel?> GetHotelById(int id)
        {
            var hotel = await hotelRepository.GetHotelById(id);

            return mapper.Map<GetHotel>(hotel);
        }
        public async Task<GetHotel> CreateHotel(CreateHotel hotel)
        {
            // Validate the input DTO
            var validationResult = await CreateHotelValidator.ValidateAsync(hotel);

            // Check if validation failed
            if (!validationResult.IsValid)
            {
                // Throw an exception with validation errors
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            // Map the CreateHotel DTO to the Hotel entity
            var hotelEntity = mapper.Map<Hotel>(hotel);

            // Create the hotel in the repository
            var createdHotel = await hotelRepository.CreateHotel(hotelEntity);

            // Map the created Hotel entity to the GetHotel DTO
            return mapper.Map<GetHotel>(createdHotel);
        }
        public async Task<GetHotel?> UpdateHotel(int id, UpdateHotel hotel)
        {
            // Validate the input DTO
            var validationResult = await updateHotelValidator.ValidateAsync(hotel);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            var hotelEntity = mapper.Map<Hotel>(hotel);
            var updateHotelResult = await hotelRepository.UpdateHotel(id, hotelEntity);

            return mapper.Map<GetHotel>(updateHotelResult);
        }

        public async Task<bool> DeleteHotel(int id)
        {
            var deleteResult = await hotelRepository.DeleteHotel(id);

            return deleteResult;
        }
    }
}
