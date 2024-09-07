using FluentValidation;

namespace Backend.Features.Register;

public class Validator : Validator<RegisterReq>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ContactNumber).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
