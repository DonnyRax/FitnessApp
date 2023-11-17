using FluentValidation;

namespace API.Weight.SubmitWeight;

public class SubmitWeightRequestValidator : AbstractValidator<SubmitWeightRequest>
{
    public SubmitWeightRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");

        RuleFor(x => x.Weight)
            .NotEmpty().WithMessage("Weight is required")
            .GreaterThan(0).WithMessage("Value must be greater than 0");
    }
}
