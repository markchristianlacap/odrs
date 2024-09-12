using FluentValidation;

namespace Backend.Features.User.UpdateProfile;

public class Validator : Validator<UpdateProfileReq>
{
    public Validator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.ContactNumber).NotEmpty();
        RuleFor(x => x.Birthdate).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
    }
}
