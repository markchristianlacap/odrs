using FluentValidation;

namespace Backend.Features.Requests.Store;

public class Validator : Validator<RequestReq>
{
    public Validator()
    {
        RuleFor(x => x.DocumentType).NotNull().IsInEnum();
        RuleFor(x => x.LastAttendanceStartYear).NotEmpty();
        RuleFor(x => x.LastAttendanceEndYear).NotEmpty();
        RuleFor(x => x.Semester).NotNull().IsInEnum();
        RuleFor(x => x.YearLevel).NotNull().IsInEnum();
        RuleFor(x => x.RequesterType).NotNull().IsInEnum();
        RuleFor(x => x.Section).NotEmpty();
        RuleFor(x => x.ProgramId).NotEmpty();
        RuleFor(x => x.CampusId).NotEmpty();
        RuleFor(x => x.Purpose).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.ContactNumber).NotEmpty();
        RuleFor(x => x.Birthdate).NotNull();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.StudentNumber).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
