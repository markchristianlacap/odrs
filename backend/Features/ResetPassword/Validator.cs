using FluentValidation;

namespace Backend.Features.ResetPassword;

public class Validator : Validator<ResetPasswordReq>
{
    public Validator()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match");
    }
}
