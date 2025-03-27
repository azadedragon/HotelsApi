using FluentValidation;
using HotelsApi.Dtos;
using HotelsApi.Repositories;

namespace HotelsApi.Validator
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransaction>
    {
        public CreateTransactionValidator(ITransactionRepository transactionRepository, IHotelRepository hotelRepository) 
        {
            RuleFor(x => x.HotelId)
            .NotEmpty()
            .WithMessage("Hotel ID is required")
            .NotNull()
            .WithMessage("Hotel ID is required")
            .MustAsync(async (id, cancellation) => await hotelRepository.HotelIdExists(id, cancellation))
            .WithMessage("Hotel does not exist");
            RuleFor(x => x.HotelCode)
                .NotEmpty()
                .WithMessage("Hotel Code is required")
                .NotNull()
                .WithMessage("Hotel Code is required");
            RuleFor(x => x.HotelName)
                .NotEmpty()
                .WithMessage("Hotel Name is required")
                .NotNull()
                .WithMessage("Hotel Name is required");
        }
        public class UpdateTransactionValidator : AbstractValidator<UpdateTransaction>
        {
            public UpdateTransactionValidator(ITransactionRepository transactionRepository, IHotelRepository hotelRepository)
            {
                RuleFor(x => x.HotelId)
                    .NotEmpty()
                    .WithMessage("Hotel ID is required")
                    .NotNull()
                    .WithMessage("Hotel ID is required")
                    .MustAsync(async (id, cancellation) => await hotelRepository.HotelIdExists(id, cancellation))
                    .WithMessage("Hotel does not exist");
                RuleFor(x => x.HotelCode)
                    .NotEmpty()
                    .WithMessage("Hotel Code is required")
                    .NotNull()
                    .WithMessage("Hotel Code is required");
                RuleFor(x => x.HotelName)
                    .NotEmpty()
                    .WithMessage("Hotel Name is required")
                    .NotNull()
                    .WithMessage("Hotel Name is required");
            }
        }
    }
}
