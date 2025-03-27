using AutoMapper;
using FluentValidation;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using HotelsApi.Repositories;
using HotelsApi.Validator;

namespace HotelsApi.Services
{
    public interface ICityService
    {
        Task<List<GetCity>> GetAllCity();
        Task<GetCity?> GetCityById(int id);
        Task<GetCity> CreateCity(CreateCity city);
        Task<GetCity?> UpdateCity(int id, UpdateCity city);
        Task<bool> DeleteCity(int id);
    }
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;
        private readonly IValidator<CreateCity> CreateCityValidator;
        private readonly IValidator<UpdateCity> updateCityValidator;
        private readonly IMapper mapper;
        public CityService(ICityRepository cityRepository, IMapper mapper, IValidator<CreateCity> CreateCityValidator, IValidator<UpdateCity> updateCityValidator)
        {
            this.cityRepository = cityRepository;
            this.mapper = mapper;
            this.CreateCityValidator = CreateCityValidator;
            this.updateCityValidator = updateCityValidator;
        }
        public async Task<List<GetCity>> GetAllCity()
        {
            var city = await cityRepository.GetAllCity();
            return mapper.Map<List<GetCity>>(city);
        }
        public async Task<GetCity?> GetCityById(int id)
        {
            var city = await cityRepository.GetCityById(id);
            return mapper.Map<GetCity>(city);
        }
        public async Task<GetCity> CreateCity(CreateCity city)
        {
            var validationResult = await CreateCityValidator.ValidateAsync(city);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }
            var cityEntity = mapper.Map<Entities.City>(city);
            var createdCity = await cityRepository.CreateCity(cityEntity);
            return mapper.Map<GetCity>(createdCity);
        }
        public async Task<GetCity?> UpdateCity(int id, UpdateCity city)
        {
            // Validate the input DTO
            var validationResult = await updateCityValidator.ValidateAsync(city);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            var cityEntity = mapper.Map<City>(city);
            var updateCityResult = await cityRepository.UpdateCity(id, cityEntity);

            return mapper.Map<GetCity>(updateCityResult);
        }
        public async Task<bool> DeleteCity(int id)
        {
            return await cityRepository.DeleteCity(id);
        }
    }
}
