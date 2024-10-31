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
        RuleFor(x => x.Documents).NotNull();
        RuleForEach(x => x.Documents).SetValidator(new DocumentValidator());
        RuleFor(x => x.LastAttendanceStartYear).NotEmpty();
        RuleFor(x => x.LastAttendanceEndYear).NotEmpty();
        RuleFor(x => x.Semester).NotNull().IsInEnum();
        RuleFor(x => x.YearLevel).NotNull().IsInEnum();
        RuleFor(x => x.RequesterType).NotNull().IsInEnum();
        RuleFor(x => x.Section).NotEmpty();
        RuleFor(x => x.ProgramId).NotEmpty();
        RuleFor(x => x.CampusId).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.ContactNumber).NotEmpty();
        RuleFor(x => x.Birthdate).NotNull();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.StudentNumber).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Picture).NotNull();
        RuleFor(x => x.ValidId).NotNull();
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
