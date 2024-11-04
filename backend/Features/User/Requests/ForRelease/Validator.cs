using FluentValidation;

namespace Backend.Features.User.Requests.ForRelease;

public class Validator : Validator<ReleaseDocumentsReq>
{
    public Validator()
    {
        RuleFor(x => x.ClaimDeadline).NotEmpty();
    }
}
