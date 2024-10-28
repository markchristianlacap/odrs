using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Backend.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Requests.Store;

public class Endpoint : Endpoint<RequestReq, RequestRes>
{
    public AppDbContext Db { get; set; } = null!;
    public IStorageService StorageService { get; set; } = null!;
    public IEmailService EmailService { get; set; } = null!;

    public override void Configure()
    {
        Post("/requests");
        AllowAnonymous();
        AllowFormData();
        AllowFileUploads();
    }

    public override async Task HandleAsync(RequestReq req, CancellationToken ct)
    {
        var requirements = new List<RequestRequirement>();
        var request = req.Adapt<Request>();
        if (
            req.AuthorizationLetter != null
            && req.CollectorType == CollectorType.ImmediateFamilyMember
        )
        {
            var path = await StorageService.UploadFileAsync(
                req.AuthorizationLetter,
                "requirements",
                ct
            );
            requirements.Add(
                new RequestRequirement
                {
                    RequestId = request.Id,
                    Path = path,
                    Type = RequirementType.AuthorizationLetter,
                }
            );
        }
        if (req.ValidId != null && req.CollectorType != CollectorType.Owner)
        {
            var path = await StorageService.UploadFileAsync(req.ValidId, "requirements", ct);
            requirements.Add(
                new RequestRequirement
                {
                    RequestId = request.Id,
                    Path = path,
                    Type = RequirementType.ValidId,
                }
            );
        }
        if (req.RepresentativeValidId != null && req.CollectorType != CollectorType.Owner)
        {
            var path = await StorageService.UploadFileAsync(
                req.RepresentativeValidId,
                "requirements",
                ct
            );
            requirements.Add(
                new RequestRequirement
                {
                    RequestId = request.Id,
                    Path = path,
                    Type = RequirementType.RepresentativeValidId,
                }
            );
        }
        if (
            req.CollectorType == CollectorType.AuthorizedRepresentative
            && req.RepresentativeValidId != null
        )
        {
            var path = await StorageService.UploadFileAsync(
                req.RepresentativeValidId,
                "requirements",
                ct
            );
            requirements.Add(
                new RequestRequirement
                {
                    RequestId = request.Id,
                    Path = path,
                    Type = RequirementType.RepresentativeValidId,
                }
            );
        }
        if (
            req.Documents.Any(x => x.Type == DocumentType.SecondCopyOfDiploma)
            && req.AffidavitOfLoss != null
            && req.BirthCertificate != null
        )
        {
            var path = await StorageService.UploadFileAsync(
                req.AffidavitOfLoss,
                "requirements",
                ct
            );
            requirements.Add(
                new RequestRequirement
                {
                    RequestId = request.Id,
                    Path = path,
                    Type = RequirementType.AffidavitOfLoss,
                }
            );
            path = await StorageService.UploadFileAsync(req.BirthCertificate, "requirements", ct);
            requirements.Add(
                new RequestRequirement
                {
                    RequestId = request.Id,
                    Path = path,
                    Type = RequirementType.BirthCertificate,
                }
            );
        }
        if (
            req.Documents.Any(x => x.Type == DocumentType.HonorableDismissal)
            && req.RequestLetter != null
        )
        {
            var path = await StorageService.UploadFileAsync(req.RequestLetter, "requirements", ct);
            requirements.Add(
                new RequestRequirement
                {
                    RequestId = request.Id,
                    Path = path,
                    Type = RequirementType.RequestLetter,
                }
            );
        }
        request.Requirements = requirements;
        request.Histories =
        [
            new RequestHistory
            {
                RequestId = request.Id,
                Remarks =
                    "Request submitted. Waiting for admin validation before proceeding with payment.",
                RequestStatus = RequestStatus.Submitted,
            },
        ];
        request.ReferenceNumber = await GenerateReferenceNumber(ct);
        request.Status = RequestStatus.Submitted;
        request.PicturePath = await StorageService.UploadFileAsync(req.Picture, "pictures", ct);
        await Db.Requests.AddAsync(request, ct);
        await Db.SaveChangesAsync(ct);
        SendEmailNotification(req.Email);
        Response = request.Adapt<RequestRes>();
    }

    private async Task<string> GenerateReferenceNumber(CancellationToken ct)
    {
        var year = DateTime.Now.Year;
        var month = DateTime.Now.Month;
        var lastNum = await Db
            .Requests.Where(x => x.CreatedAt.Year == year && x.CreatedAt.Month == month)
            .OrderByDescending(x => x.ReferenceNumber)
            .Select(x => x.ReferenceNumber)
            .FirstOrDefaultAsync(ct);
        lastNum = lastNum?[6..];
        var newNum = lastNum == null ? 1 : int.Parse(lastNum) + 1;
        return $"{year}{month:00}{newNum:0000}";
    }

    private void SendEmailNotification(string emailAddress)
    {
        var subject = "Request Submitted";
        var body =
            @$"
            <p>Hello,</p>
            <p>You have submitted a request.</p>
            <p>Please wait for admin validation before proceeding with payment.</p>
            <p>Thank you.</p>
            ";
        EmailService.SendEmail(emailAddress, subject, body, isHtml: true);
    }
}
