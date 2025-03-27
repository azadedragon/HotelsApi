using FluentValidation;
using HotelsApi.Dtos;
using HotelsApi.Repositories;

namespace HotelsApi.Validator
{
    public class CreateHotelValidator : AbstractValidator<CreateHotel>
    {
        public CreateHotelValidator(IHotelRepository hotelRepository, IBarangayRepository barangayRepository)
        {
            RuleFor(x => x.HotelCode)
                .NotNull()
                .WithMessage("Hotel Code is required")
                .NotEmpty()
                .WithMessage("Hotel Code is required")
                .MustAsync(async (hotelCode, cancellationToken) =>
                {
                    // Check if HotelCode already exists in the database
                    return !await hotelRepository.HotelCodeExists(hotelCode, cancellationToken);
                })
                .WithMessage("Hotel Code must be unique");

            RuleFor(x => x.HotelName)
                .NotNull()
                .WithMessage("Hotel Name is required")
                .NotEmpty()
                .WithMessage("Hotel Name is required")
                .MustAsync(async (hotelName, cancellationToken) =>
                {
                    // Check if HotelName already exists in the database
                    return !await hotelRepository.HotelNameExists(hotelName, cancellationToken);
                })
                .WithMessage("Hotel Name must be unique");

            RuleFor(x => x.HotelDescription)
                .NotNull()
                .WithMessage("Hotel Description is required")
                .NotEmpty()
                .WithMessage("Hotel Description is required");

            RuleFor(x => x.BarangayId)
                .NotNull()
                .WithMessage("Barangay Id is required")
                .NotEmpty()
                .WithMessage("Barangay Id is required")
                .MustAsync(async (barangayId, cancellationToken) =>
                 {
                     return await barangayRepository.BarangayExistsAsync(barangayId, cancellationToken);
                 })
                    .WithMessage("Barangay Id doesn't exist");
        }
    }

    public class UpdateHotelValidator : AbstractValidator<UpdateHotel>
    {
        public UpdateHotelValidator(IHotelRepository hotelRepository, IBarangayRepository barangayRepository)
        {
            RuleFor(x => x.HotelCode)
                .NotNull()
                .WithMessage("Hotel Code is required")
                .NotEmpty()
                .WithMessage("Hotel Code is required")
               .MustAsync(async (model, hotelCode, context, cancellationToken) =>
               {
                   // Get the current hotel from database
                   var currentHotel = await hotelRepository.GetHotelById(model.HotelId);
                   if (currentHotel == null) return false;

                   // Only check uniqueness if the code is being changed
                   if (currentHotel.HotelCode == hotelCode) return true;

                   return !await hotelRepository.HotelCodeExists(hotelCode, cancellationToken);
               })
            .WithMessage("Hotel Code must be unique");

            RuleFor(x => x.HotelName)
                .NotNull()
                .WithMessage("Hotel Name is required")
                .NotEmpty()
                .WithMessage("Hotel Name is required")
                .MustAsync(async (model, hotelName, context, cancellationToken) =>
                {
                    // Get the current hotel from database
                    var currentHotel = await hotelRepository.GetHotelById(model.HotelId);
                    if (currentHotel == null) return false;

                    // Only check uniqueness if the name is being changed
                    if (currentHotel.HotelName == hotelName) return true;

                    return !await hotelRepository.HotelNameExists(hotelName, cancellationToken);
                })
            .WithMessage("Hotel Name must be unique");

            RuleFor(x => x.HotelDescription)
                .NotNull()
                .WithMessage("Hotel Description is required")
                .NotEmpty()
                .WithMessage("Hotel Description is required");

            RuleFor(x => x.BarangayId)
                .NotNull()
                .WithMessage("Barangay Id is required")
                .NotEmpty()
                .WithMessage("Barangay Id is required")
                .MustAsync(async (barangayId, cancellationToken) =>
                {
                    return await barangayRepository.BarangayExistsAsync(barangayId, cancellationToken);
                })
                    .WithMessage("Barangay Id doesn't exist");
        }
    }
}