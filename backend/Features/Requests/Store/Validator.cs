using System.Text.RegularExpressions;
using Backend.Enums;
using FluentValidation;

namespace Backend.Features.Requests.Store;

public class DocumentValidator : AbstractValidator<RequestDocumentModel>
{
    public DocumentValidator()
    {
        RuleFor(x => x.Type).NotNull().IsInEnum();
        RuleFor(x => x.Copies).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Purpose).NotEmpty();
    }
}

public class Validator : Validator<RequestReq>
{
    public Validator()
    {
        RuleFor(x => x.Documents).NotNull().NotEmpty();
        RuleForEach(x => x.Documents).SetValidator(new DocumentValidator());
        RuleFor(x => x.LastAttendanceStartYear)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Last Attendance Start Year invalid");
        RuleFor(x => x.LastAttendanceEndYear)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Last Attendance End Year invalid");
        RuleFor(x => x.Semester).NotNull().IsInEnum();

        RuleFor(x => x.RequesterType).NotNull().IsInEnum();
        RuleFor(x => x.ProgramId)
            .NotEmpty()
            .Must(x => x != new Guid())
            .WithMessage("Program is required");
        RuleFor(x => x.CampusId)
            .NotEmpty()
            .Must(x => x != new Guid())
            .WithMessage("Campus is required");
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.ContactNumber)
            .NotEmpty()
            .Must(x =>
            {
                // check if it is a valid number +63-900-000-0000
                return Regex.IsMatch(x, @"^\+63-\d{3}-\d{3}-\d{4}$");
            })
            .WithMessage("Invalid contact number");
        RuleFor(x => x.Birthdate)
            .NotEmpty()
            .Must(x => x < DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Invalid birthdate")
            .Must(x => x != DateOnly.MinValue)
            .WithMessage("Invalid birthdate");
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.ValidId).NotNull();
        When(
            x => x.Documents.Any(x => x.Type == DocumentType.TOR),
            () =>
            {
                RuleFor(x => x.Picture).NotNull();
            }
        );
        When(
            x => x.RequesterType == RequesterType.FormerStudent,
            () =>
            {
                RuleFor(x => x.YearLevel).NotNull().IsInEnum();
            }
        );
        When(
            x => x.CollectorType != CollectorType.Owner,
            () =>
            {
                RuleFor(x => x.RepresentativeValidId).NotNull();
                RuleFor(x => x.ValidId).NotNull();
            }
        );
        When(
            x => x.CollectorType == CollectorType.AuthorizedRepresentative,
            () =>
            {
                RuleFor(x => x.SpecialPowerOfAttorney).NotNull();
            }
        );
        When(
            x => x.CollectorType == CollectorType.ImmediateFamilyMember,
            () =>
            {
                RuleFor(x => x.AuthorizationLetter).NotNull();
            }
        );
        When(
            x => x.Documents.Any(x => x.Type == DocumentType.SecondCopyOfDiploma),
            () =>
            {
                RuleFor(x => x.AffidavitOfLoss).NotNull();
                RuleFor(x => x.BirthCertificate).NotNull();
            }
        );
        When(
            x => x.CollectorType != CollectorType.Owner,
            () => RuleFor(x => x.Representative).NotEmpty()
        );
        When(
            x => x.Documents.Any(x => x.Type == DocumentType.HonorableDismissal),
            () => RuleFor(x => x.RequestLetter).NotNull()
        );
        When(
            x => x.Documents.Any(x => x.Type == DocumentType.Authentication),
            () =>
            {
                RuleFor(x => x.DocumentToBeCertified).NotNull();
            }
        );
    }
}
