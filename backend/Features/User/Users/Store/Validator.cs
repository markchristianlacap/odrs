using FluentValidation;

namespace Backend.Features.User.Users.Store;

public class Validator : Validator<UserStoreReq>
{
    public Validator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.ContactNumber).NotEmpty();
        RuleFor(x => x.Birthdate).NotNull();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.Role).NotNull().IsInEnum();
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .MinimumLength(8)
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match");
    }
}
