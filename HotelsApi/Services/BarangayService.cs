using AutoMapper;
using FluentValidation;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using HotelsApi.Repositories;
using static HotelsApi.Validator.CreateBarangayValidator;

namespace HotelsApi.Services
{
    public interface IBarangayService
    {
        Task<List<GetBarangay>> GetAllBarangay();
        Task<GetBarangay?> GetBarangayById(int id);
        Task<GetBarangay> CreateBarangay(CreateBarangay barangay);
        Task<GetBarangay?> UpdateBarangay(int id, UpdateBarangay barangay);
        Task<bool> DeleteBarangay(int id);
    }
    public class BarangayService : IBarangayService
    {
        private readonly IBarangayRepository barangayRepository;
        private readonly IValidator<CreateBarangay> CreateBarangayValidator;
        private readonly IValidator<UpdateBarangay> updateBarangayValidator;
        private readonly IMapper mapper;
        public BarangayService(IBarangayRepository barangayRepository, IMapper mapper, IValidator<CreateBarangay> CreateBarangayValidator, IValidator<UpdateBarangay> updateBarangayValidator)
        {
            this.barangayRepository = barangayRepository;
            this.mapper = mapper;
            this.CreateBarangayValidator = CreateBarangayValidator;
            this.updateBarangayValidator = updateBarangayValidator;
        }
        public async Task<List<GetBarangay>> GetAllBarangay()
        {
            var barangay = await barangayRepository.GetAllBarangay();
            return mapper.Map<List<GetBarangay>>(barangay);
        }
        public async Task<GetBarangay?> GetBarangayById(int id)
        {
            var barangay = await barangayRepository.GetBarangayById(id);
            return mapper.Map<GetBarangay>(barangay);
        }
        public async Task<GetBarangay> CreateBarangay(CreateBarangay barangay)
        {
            var validationResult = await CreateBarangayValidator.ValidateAsync(barangay);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }
            var barangayEntity = mapper.Map<Entities.Barangay>(barangay);
            var createdBarangay = await barangayRepository.CreateBarangay(barangayEntity);
            return mapper.Map<GetBarangay>(createdBarangay);
        }
        public async Task<GetBarangay?> UpdateBarangay(int id, UpdateBarangay barangay)
        {
            // Validate the input DTO
            var validationResult = await updateBarangayValidator.ValidateAsync(barangay);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            var barangayEntity = mapper.Map<Barangay>(barangay);
            var updateBarangayResult = await barangayRepository.UpdateBarangay(id, barangayEntity);

            return mapper.Map<GetBarangay>(updateBarangayResult);
        }
        public async Task<bool> DeleteBarangay(int id)
        {
            return await barangayRepository.DeleteBarangay(id);
        }
    }
}
