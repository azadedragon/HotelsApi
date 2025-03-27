using FluentValidation;
using HotelsApi.Dtos;
using HotelsApi.Repositories;

namespace HotelsApi.Validator
{
    public class CreateCountryValidator : AbstractValidator<CreateCountry>
    {
        public CreateCountryValidator(ICountryRepository countryRepository)
        {
            RuleFor(x => x.CountryName)
                .NotEmpty()
                .WithMessage("Country name is required")
                .MaximumLength(50)
                .WithMessage("Country name must not exceed 50 characters")
               .MustAsync(async (countryName, cancellationToken) =>
               {
                   // Check if countryName already exists in the database
                   return !await countryRepository.CountryNameExists(countryName, cancellationToken);
               })
                .WithMessage("Country name already exists");
            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("Country code is required")
                .MaximumLength(5)
                .WithMessage("Country code must not exceed 5 characters")
               .WithMessage("Country Code must not exceed 5 characters")
                .MustAsync(async (countryCode, cancellationToken) =>
                {
                    // Check if countryCode already exists in the database
                    return !await countryRepository.CountryCodeExists(countryCode, cancellationToken);
                })
                .WithMessage("Country code already exists");
        }
    }
    public class UpdateCountryValidator : AbstractValidator<UpdateCountry>
    {
        public UpdateCountryValidator(ICountryRepository countryRepository)
        {
            RuleFor(x => x.CountryName)
                .NotEmpty()
                .WithMessage("Country name is required")
                .MaximumLength(50)
                .WithMessage("Country name must not exceed 50 characters")
               .MustAsync(async (model, countryName, context, cancellationToken) =>
               {
                   // Get the current hotel from database
                   var currentCountry = await countryRepository.GetCountryById(model.CountryId);
                   if (currentCountry == null) return false;

                   // Only check uniqueness if the name is being changed
                   if (currentCountry.CountryName == countryName) return true;

                   return !await countryRepository.CountryNameExists(countryName, cancellationToken);
               })
                .WithMessage("Country name already exists");
            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("Country code is required")
                .MaximumLength(5)
                .WithMessage("Country Code must not exceed 5 characters")
               .MustAsync(async (model, countryCode, context, cancellationToken) =>
               {
                   // Get the current hotel from database
                   var currentCountry = await countryRepository.GetCountryById(model.CountryId);
                   if (currentCountry == null) return false;

                   // Only check uniqueness if the name is being changed
                   if (currentCountry.CountryCode == countryCode) return true;

                   return !await countryRepository.CountryCodeExists(countryCode, cancellationToken);
               })
                .WithMessage("Country code already exists");
        }
    }
}
