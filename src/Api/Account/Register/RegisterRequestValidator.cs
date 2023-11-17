using FluentValidation;

namespace API.Account.Register;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("Display Name is required.");
        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("Email Address is required.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(9).WithMessage("Your password length must be at least 9.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[!?*.#&^$@<>:%]+").WithMessage("Your password must contain at least one (!?*.#&^$@<>:%).");
    }
}
