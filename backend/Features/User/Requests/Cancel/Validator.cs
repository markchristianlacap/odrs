using FluentValidation;

namespace Backend.Features.User.Requests.Cancel;

public class Validator : Validator<CancelRequestReq>
{
    public Validator()
    {
        RuleFor(x => x.Remarks).NotEmpty();
    }
}
