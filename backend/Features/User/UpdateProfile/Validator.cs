using FluentValidation;

namespace Backend.Features.User.UpdateProfile;

public class Validator : Validator<UpdateProfileReq>
{
  public Validator()
  {
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.ContactNumber).NotEmpty();
  }
}
