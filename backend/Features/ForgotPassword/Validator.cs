using FluentValidation;

namespace Backend.Features.ForgotPassword;

public class Validator : Validator<ForgotPasswordReq>
{
    public Validator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
