using FluentValidation;

namespace Backend.Features.User.Documents.Store;

public class Validator : Validator<DocumentRequestReq>
{
  public Validator()
  {
    RuleFor(x => x.DocumentType).NotNull().IsInEnum();
    RuleFor(x => x.CopyType).NotNull().IsInEnum();
    RuleFor(x => x.LastAttendanceStartYear).NotEmpty();
    RuleFor(x => x.LastAttendanceEndYear).NotEmpty();
    RuleFor(x => x.LastAttendanceSemester).NotNull().IsInEnum();
    RuleFor(x => x.LastAttendanceYearLevel).NotNull().IsInEnum();
    RuleFor(x => x.LastAttendanceSection).NotEmpty();
    RuleFor(x => x.Purpose).NotEmpty();
    RuleFor(x => x.FirstName).NotEmpty();
    RuleFor(x => x.LastName).NotEmpty();
    RuleFor(x => x.ContactNumber).NotEmpty();
    RuleFor(x => x.Birthdate).NotEmpty();
    RuleFor(x => x.Address).NotEmpty();
    RuleFor(x => x.IsGraduate).NotNull();
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
  }
}
