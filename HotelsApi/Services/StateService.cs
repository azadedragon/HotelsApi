using AutoMapper;
using FluentValidation;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using HotelsApi.Repositories;
using HotelsApi.Validator;

namespace HotelsApi.Services
{
    public interface IStateService
    {
        Task<List<GetState>> GetAllStates();
        Task<GetState?> GetStateById(int id);
        Task<GetState> CreateState(CreateState state);
        Task<GetState?> UpdateState(int id, UpdateState state);
        Task<bool> DeleteState(int id);
    }
    public class StateService : IStateService
    {
        private readonly IStateRepository stateRepository;
        private readonly IValidator<CreateState> CreateStateValidator;
        private readonly IValidator<UpdateState> UpdateStateValidator;
        private readonly IMapper mapper;
        public StateService(IStateRepository stateRepository, IMapper mapper, IValidator<CreateState> CreateStateValidator, IValidator<UpdateState> UpdateStateValidator)
        {
            this.stateRepository = stateRepository;
            this.mapper = mapper;
            this.CreateStateValidator = CreateStateValidator;
            this.UpdateStateValidator = UpdateStateValidator;
        }

        public async Task<GetState> CreateState(CreateState state)
        {
            // Validate the input DTO
            var validationResult = await CreateStateValidator.ValidateAsync(state);

            // Check if validation failed
            if (!validationResult.IsValid)
            {
                // Throw an exception with validation errors
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            // Map the CreateHotel DTO to the Hotel entity
            var stateEntity = mapper.Map<State>(state);

            // Create the state in the repository
            var createdState = await stateRepository.CreateState(stateEntity);

            // Map the created state entity to the GetHotel DTO
            return mapper.Map<GetState>(createdState);
        }

        public async Task<bool> DeleteState(int id)
        {
            var deleteResult = await stateRepository.DeleteState(id);

            return deleteResult;
        }

        public async Task<List<GetState>> GetAllStates()
        {
            var state = await stateRepository.GetAllStates();

            return mapper.Map<List<GetState>>(state);
        }

        public async Task<GetState?> GetStateById(int id)
        {
            var state = await stateRepository.GetStateById(id);

            return mapper.Map<GetState>(state);
        }

        public async Task<GetState?> UpdateState(int id, UpdateState state)
        {
            // Validate the input DTO
            var validationResult = await UpdateStateValidator.ValidateAsync(state);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            var stateEntity = mapper.Map<State>(state);
            var updateStateResult = await stateRepository.UpdateState(id, stateEntity);

            return mapper.Map<GetState>(updateStateResult);
        }
    }
}
