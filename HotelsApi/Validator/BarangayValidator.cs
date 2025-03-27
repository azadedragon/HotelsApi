using HotelsApi.Dtos;
using HotelsApi.Repositories;
using FluentValidation;

namespace HotelsApi.Validator
{
    public class CreateBarangayValidator : AbstractValidator<CreateBarangay>
    {
        public CreateBarangayValidator(IBarangayRepository barangayRepository, ICityRepository cityRepository)
        {
            RuleFor(x => x.PostalCode)
                .NotNull()
                .WithMessage("Postal Code is required")
                .NotEmpty()
                .WithMessage("Postal Code is required");
            RuleFor(x => x.BarangayName)
                .NotNull()
                .WithMessage("Barangay Name is required")
                .NotEmpty()
                .WithMessage("Barangay Name is required")
                .MustAsync(async (barangayName, cancellationToken) =>
                {
                    // Check if BarangayName already exists in the database
                    return !await barangayRepository.BarangayNameExists(barangayName, cancellationToken);
                })
                .WithMessage("Barangay Name must be unique");
            RuleFor(x => x.CityId)
                .NotNull()
                .WithMessage("City Id is required")
                .NotEmpty()
                .WithMessage("City Id is required")
                    .MustAsync(async (cityId, cancellationToken) =>
                    {
                        return await cityRepository.CityExistsAsync(cityId, cancellationToken);
                    })
                    .WithMessage("City Id doesn't exist");
        }
    }

}


public class UpdateBarangayValidator : AbstractValidator<UpdateBarangay>
{
    public UpdateBarangayValidator(IBarangayRepository barangayRepository, ICityRepository cityRepository)
    {
        RuleFor(x => x.PostalCode)
            .NotNull()
            .WithMessage("Postal Code is required")
            .NotEmpty()
            .WithMessage("Postal Code is required");
        RuleFor(x => x.BarangayName)
            .NotNull()
            .WithMessage("Barangay Name is required")
            .NotEmpty()
            .WithMessage("Barangay Name is required")
            .MustAsync(async (model, barangayName, context, cancellationToken) =>
            {
                // Get the current hotel from database
                var currentBarangay = await barangayRepository.GetBarangayById(model.BarangayId);
                if (currentBarangay == null) return false;

                // Only check uniqueness if the name is being changed
                if (currentBarangay.BarangayName == barangayName) return true;

                return !await barangayRepository.BarangayNameExists(barangayName, cancellationToken);
            })
            .WithMessage("Barangay Name must be unique");
        RuleFor(x => x.CityId)
            .NotNull()
            .WithMessage("City Id is required")
            .NotEmpty()
            .WithMessage("City Id is required")
             .MustAsync(async (cityId, cancellationToken) =>
             {
                 return await cityRepository.CityExistsAsync(cityId, cancellationToken);
             })
                .WithMessage("City Id doesn't exist");
    }
}

