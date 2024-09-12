using FluentValidation;

namespace Backend.Features.Login;

public class Validator : Validator<LoginReq>
{
    public Validator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
