using HotelsApi.Dtos;
using HotelsApi.Repositories;
using FluentValidation;

namespace HotelsApi.Validator
{
    public class CreateCityValidator : AbstractValidator<CreateCity>
    {
        public CreateCityValidator(ICityRepository cityRepository, IStateRepository stateRepository)
        {
            RuleFor(x => x.CityCode)
                .MustAsync(async (cityCode, cancellationToken) =>
                {
                    // Check if CityCode already exists in the database
                    return !await cityRepository.CityCodeExists(cityCode, cancellationToken);
                })
                .WithMessage("City Code must be unique");
            RuleFor(x => x.CityName)
                .NotNull()
                .WithMessage("City Name is required")
                .NotEmpty()
                .WithMessage("City Name is required")
                .MustAsync(async (cityName, cancellationToken) =>
                {
                    // Check if CityName already exists in the database
                    return !await cityRepository.CityNameExists(cityName, cancellationToken);
                })
                .WithMessage("City Name must be unique");
            RuleFor(x => x.StateId)
                .NotNull()
                .WithMessage("State Id is required")
                .NotEmpty()
                .WithMessage("State Id is required")
                    .MustAsync(async (stateId, cancellationToken) =>
                    {
                        return await stateRepository.StateExistsAsync(stateId, cancellationToken);
                    })
                    .WithMessage("StateId doesn't exist");
        }
    }

}
    

    public class UpdateCityValidator : AbstractValidator<UpdateCity>
    {
        public UpdateCityValidator(ICityRepository cityRepository, IStateRepository stateRepository)
        {
            RuleFor(x => x.CityCode)
                .MustAsync(async (model, cityCode, context, cancellationToken) =>
                {
                    // Get the current hotel from database
                    var currentCity = await cityRepository.GetCityById(model.CityId);
                    if (currentCity == null) return false;

                    // Only check uniqueness if the name is being changed
                    if (currentCity.CityCode == cityCode) return true;

                    return !await cityRepository.CityCodeExists(cityCode, cancellationToken);
                })
                    .WithMessage("City Code must be unique");
        RuleFor(x => x.CityName)
                .NotNull()
                .WithMessage("City Name is required")
                .NotEmpty()
                .WithMessage("City Name is required")
                .MustAsync(async (model, cityName, context, cancellationToken) =>
                 {
                     // Get the current hotel from database
                     var currentCity = await cityRepository.GetCityById(model.CityId);
                     if (currentCity == null) return false;

                     // Only check uniqueness if the name is being changed
                     if (currentCity.CityName == cityName) return true;

                     return !await cityRepository.CityNameExists(cityName, cancellationToken);
                 })
                    .WithMessage("City Name must be unique");
        RuleFor(x => x.StateId)
                .NotNull()
                .WithMessage("State Id is required")
                .NotEmpty()
                .WithMessage("State Id is required")
                 .MustAsync(async (stateId, cancellationToken) =>
                 {
                     return await stateRepository.StateExistsAsync(stateId, cancellationToken);
                 })
                    .WithMessage("StateId doesn't exist");
    }
    }

