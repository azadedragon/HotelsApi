using AutoMapper;
using FluentValidation;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using HotelsApi.Repositories;
using HotelsApi.Validator;

namespace HotelsApi.Services
{
    public interface ICountryService
    {
        Task<List<GetCountry>> GetAllCountry();
        Task<GetCountry?> GetCountryById(int id);
        Task<GetCountry> CreateCountry(CreateCountry country);
        Task<GetCountry?> UpdateCountry(int id, UpdateCountry country);
        Task<bool> DeleteCountry(int id);
    }
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;
        private readonly IValidator<CreateCountry> CreateCountryValidator;
        private readonly IValidator<UpdateCountry> UpdateCountryValidator;
        private readonly IMapper mapper;
        public CountryService(ICountryRepository countryRepository, IMapper mapper, IValidator<CreateCountry> CreateCountryValidator, IValidator<UpdateCountry> UpdateCountryValidator)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
            this.CreateCountryValidator = CreateCountryValidator;
            this.UpdateCountryValidator = UpdateCountryValidator;
        }
        public async Task<List<GetCountry>> GetAllCountry()
        {
            var country = await countryRepository.GetAllCountry();
            return mapper.Map<List<GetCountry>>(country);
        }
        public async Task<GetCountry?> GetCountryById(int id)
        {
            var country = await countryRepository.GetCountryById(id);
            return mapper.Map<GetCountry>(country);
        }
        public async Task<GetCountry> CreateCountry(CreateCountry country)
        {
            var validationResult = await CreateCountryValidator.ValidateAsync(country);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }
            var countryEntity = mapper.Map<Entities.Country>(country);
            var createdCountry = await countryRepository.CreateCountry(countryEntity);
            return mapper.Map<GetCountry>(createdCountry);
        }
        public async Task<GetCountry?> UpdateCountry(int id, UpdateCountry country)
        {
            // Set the CountryId from route parameter
            country.CountryId = id;

            // Validate the input DTO
            var validationResult = await UpdateCountryValidator.ValidateAsync(country);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            var countryEntity = mapper.Map<Country>(country);
            var updateCountryResult = await countryRepository.UpdateCountry(id, countryEntity);

            return mapper.Map<GetCountry>(updateCountryResult);
        }
        public async Task<bool> DeleteCountry(int id)
        {
            return await countryRepository.DeleteCountry(id);
        }
    }
}
