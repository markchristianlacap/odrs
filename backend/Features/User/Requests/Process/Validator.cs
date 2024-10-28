using FluentValidation;

namespace Backend.Features.User.Requests.Process;

public class Validator : Validator<ProcessReq>
{
    public Validator()
    {
        RuleFor(x => x.ORNumber).NotEmpty();
    }
}
