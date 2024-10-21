using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.Approve;

public class Endpoint : Endpoint<ApproveRequestReq>
{
    public AppDbContext Db { get; set; } = null!;
    public IEmailService EmailService { get; set; } = null!;

    public override void Configure()
    {
        Post("/user/requests/{id:guid}/approve");
    }

    public override async Task HandleAsync(ApproveRequestReq req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var request = await Db.Requests.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (request is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        request.Status = RequestStatus.WaitingForPayment;
        request.Amount = decimal.Parse(req.Amount);
        var status = new RequestHistory
        {
            RequestStatus = RequestStatus.WaitingForPayment,
            Remarks =
                "Request approved. Waiting for payment before proceeding with processing the request.",
            RequestId = id,
        };
        await Db.RequestHistories.AddAsync(status, ct);
        await Db.SaveChangesAsync(ct);
        SendEmailNotification(request.Email, request.ReferenceNumber, request.Amount);
    }

    private void SendEmailNotification(string emailAddress, string referenceNumber, decimal amount)
    {
        var subject = "Request with Reference Number: " + referenceNumber + " has been Approved";
        var body =
            @$"
            <p>Request has been approved.</p>
            <p>Please pay the requested amount of {amount}. Check the the instructions <a href='{BaseURL}requests/{referenceNumber}'>here</a>.</p>
            <p>Thank you.</p>
            ";
        EmailService.SendEmail(emailAddress, subject, body, isHtml: true);
    }
}
