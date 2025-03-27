using AutoMapper;
using FluentValidation;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using HotelsApi.Repositories;
using HotelsApi.Validator;
using static HotelsApi.Validator.CreateTransactionValidator;

namespace HotelsApi.Services
{
    public interface ITransactionService
    {
        Task<List<GetTransaction>> GetAllTransaction();
        Task<GetTransaction?> GetTransactionById(int id);
        Task<GetTransaction> CreateTransaction(CreateTransaction transaction);
        Task<GetTransaction?> UpdateTransaction(int id, UpdateTransaction transaction);
        Task<bool> DeleteTransaction(int id);
    }
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly IValidator<CreateTransaction> CreateTransactionValidator;
        private readonly IValidator<UpdateTransaction> UpdateTransactionValidator;
        private readonly IMapper mapper;
        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, IHotelRepository hotelRepository, IValidator<CreateTransaction> CreateTransactionValidator, IValidator<UpdateTransaction> UpdateTransactionValidator)
        {
            this.transactionRepository = transactionRepository;
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
            this.CreateTransactionValidator = CreateTransactionValidator;
            this.UpdateTransactionValidator = UpdateTransactionValidator;
        }
        public async Task<List<GetTransaction>> GetAllTransaction()
        {
            var transaction = await transactionRepository.GetAllTransaction();
            return mapper.Map<List<GetTransaction>>(transaction);
        }
        public async Task<GetTransaction?> GetTransactionById(int id)
        {
            var transaction = await transactionRepository.GetTransactionById(id);
            return mapper.Map<GetTransaction>(transaction);
        }
        public async Task<GetTransaction> CreateTransaction(CreateTransaction transaction)
        {
            var validationResult = await CreateTransactionValidator.ValidateAsync(transaction);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            // Additional business validation - ensure hotel exists
            if (!await hotelRepository.HotelIdExists(transaction.HotelId))
            {
                throw new KeyNotFoundException($"Hotel with ID {transaction.HotelId} not found");
            }
            // Map to entity
            var transactionEntity = mapper.Map<Entities.Transaction>(transaction);

            // Create transaction
            var createdTransaction = await transactionRepository.CreateTransaction(transactionEntity);

            // Map back to DTO
            return mapper.Map<GetTransaction>(createdTransaction);
        }
        public async Task<GetTransaction?> UpdateTransaction(int id, UpdateTransaction transaction)
        {
            // Validate the input DTO
            var validationResult = await UpdateTransactionValidator.ValidateAsync(transaction);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            var transactionEntity = mapper.Map<Transaction>(transaction);
            var updateTransactionResult = await transactionRepository.UpdateTransaction(id, transactionEntity);

            return mapper.Map<GetTransaction>(updateTransactionResult);
        }
        public async Task<bool> DeleteTransaction(int id)
        {
            return await transactionRepository.DeleteTransaction(id);
        }
    }
}
