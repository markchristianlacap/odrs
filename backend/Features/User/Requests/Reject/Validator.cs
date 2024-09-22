using FluentValidation;

namespace Backend.Features.User.Requests.Reject;

public class Validator : Validator<RejectRequestReq>
{
    public Validator()
    {
        RuleFor(x => x.Remarks).NotEmpty();
    }
}
