using FluentValidation;
using HotelsApi.Dtos;
using HotelsApi.Repositories;

namespace HotelsApi.Validator
{
    public class CreateStateValidator : AbstractValidator<CreateState>
    {
        public CreateStateValidator(IStateRepository stateRepository, ICountryRepository countryRepository)
        {
            RuleFor(x => x.StateName)
                .NotNull()
                .WithMessage("State Name is required")
                .NotEmpty()
                .WithMessage("State Name is required")
                .MaximumLength(50)
                .WithMessage("State Name must not exceed 50 characters")
                .MustAsync(async (stateName, cancellationToken) =>
                {
                    // Check if stateName already exists in the database
                    return !await stateRepository.StateNameExists(stateName, cancellationToken);
                })
                .WithMessage("State Name must be unique");

            RuleFor(x => x.StateCode)
                .MaximumLength(5)
                .WithMessage("State Code must not exceed 5 characters")
                .MustAsync(async (stateCode, cancellationToken) =>
                {
                    // Check if stateCode already exists in the database
                    return !await stateRepository.StateNameExists(stateCode, cancellationToken);
                })
                .WithMessage("State Code must be unique");
            RuleFor(x => x.CountryId)
                .NotNull()
                .WithMessage("Country Id is required")
                .NotEmpty()
                .WithMessage("Country Id is required")
                .MustAsync(async (countryId, cancellationToken) =>
                {
                    return await countryRepository.CountryExistsAsync(countryId, cancellationToken);
                })
                    .WithMessage("Country Id doesn't exist");
        }
        public class UpdateStateValidator : AbstractValidator<UpdateState>
        {
            public UpdateStateValidator(IStateRepository stateRepository, ICountryRepository countryRepository)
            {
                RuleFor(x => x.StateName)
                    .NotNull()
                    .WithMessage("State Name is required")
                    .NotEmpty()
                    .WithMessage("State Name is required")
                    .MaximumLength(50)
                    .WithMessage("State Name must not exceed 50 characters")
                    .MustAsync(async (model, stateName, context, cancellationToken) =>
                    {
                        // Get the current hotel from database
                        var currentState = await stateRepository.GetStateById(model.StateId);
                        if (currentState == null) return false;

                        // Only check uniqueness if the name is being changed
                        if (currentState.StateName == stateName) return true;

                        return !await stateRepository.StateNameExists(stateName, cancellationToken);
                    })
                    .WithMessage("State Name must be unique");

                RuleFor(x => x.StateCode)
                    .MaximumLength(5)
                    .WithMessage("State Code must not exceed 5 characters")
                    .MustAsync(async (model, stateCode, context, cancellationToken) =>
                    {
                        // Get the current hotel from database
                        var currentState = await stateRepository.GetStateById(model.StateId);
                        if (currentState == null) return false;

                        // Only check uniqueness if the name is being changed
                        if (currentState.StateCode == stateCode) return true;

                        return !await stateRepository.StateCodeExists(stateCode, cancellationToken);
                    })
                    .WithMessage("State Code must be unique");

                RuleFor(x => x.CountryId)
                    .NotNull()
                    .WithMessage("Country Id is required")
                    .NotEmpty()
                    .WithMessage("Country Id is required")
                    .MustAsync(async (countryId, cancellationToken) =>
                    {
                        return await countryRepository.CountryExistsAsync(countryId, cancellationToken);
                    })
                    .WithMessage("Country Id doesn't exist");
            }
        }
    }
}

