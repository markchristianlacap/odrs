using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.ReleaseDocuments;

public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;
    public IEmailService EmailService { get; set; } = null!;
    public IUserService UserService { get; set; } = null!;

    public override void Configure()
    {
        Post("/user/requests/{id}/release-documents");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var request = await Db
            .Requests.AsQueryable()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(ct);
        if (request == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        if (request.Status != RequestStatus.PendingForRelease)
        {
            await SendForbiddenAsync(ct);
            return;
        }
        request.Status = RequestStatus.Released;
        request.DateReleased = DateTime.Now;
        request.ReleasedById = UserService.UserId;
        var history = new RequestHistory
        {
            RequestId = id,
            Remarks = "Request released",
            RequestStatus = RequestStatus.Released,
        };
        await Db.RequestHistories.AddAsync(history, ct);
        await Db.SaveChangesAsync(ct);
        SendEmailNotification(request.Email, request.ReferenceNumber);
    }

    private void SendEmailNotification(string emailAddress, string referenceNumber)
    {
        var subject = "Request with Reference Number: " + referenceNumber + " has been Released";
        var body =
            @$"
            <p>Request has been released.</p>
            <p>Thank you for using our service.</p>
            ";
        EmailService.SendEmail(emailAddress, subject, body, isHtml: true);
    }
}
