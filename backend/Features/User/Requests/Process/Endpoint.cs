using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.Process;

public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;
    public IEmailService EmailService { get; set; } = null!;

    public override void Configure()
    {
        Post("/user/requests/{id:guid}/process");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var request = await Db.Requests.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (request is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        request.Status = RequestStatus.OnProcess;
        var status = new RequestHistory
        {
            RequestStatus = RequestStatus.OnProcess,
            Remarks =
                "Payment validated. Request is being processed. Please wait for the pickup. Thank you.",
            RequestId = id,
        };
        await Db.RequestHistories.AddAsync(status, ct);
        await Db.SaveChangesAsync(ct);
        SendEmailNotification(request.Email, request.ReferenceNumber);
    }

    private void SendEmailNotification(string emailAddress, string referenceNumber)
    {
        var subject = "Request with Reference Number: " + referenceNumber + " is On Processed";
        var body =
            @$"
        <p>Request is now being processed.</p>
        <p>We will send you an email once the request is ready for pick up.</p> 
        <p>Thank you.</p>
        ";
        EmailService.SendEmail(emailAddress, subject, body, isHtml: true);
    }
}
