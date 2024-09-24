using FluentValidation;

namespace Backend.Features.User.Requests.Approve;

public class Validator : Validator<ApproveRequestReq>
{
    public Validator()
    {
        RuleFor(x => x.Amount).NotNull().GreaterThan(0);
    }
}
