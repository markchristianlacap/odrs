using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Requests.Payment.Store;

public class Endpoint : EndpointWithoutRequest
{
    public IStorageService StorageService { get; set; } = null!;
    public AppDbContext Db { get; set; } = null!;
    public IEmailService EmailService { get; set; } = null!;

    public override void Configure()
    {
        Post("/requests/{id}/payment");
        AllowAnonymous();
        AllowFileUploads();
        AllowFormData();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var request = await Db.Requests.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (request == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        if (request.Status != RequestStatus.WaitingForPayment)
        {
            await SendForbiddenAsync(ct);
            return;
        }
        if (Files.Count == 0)
        {
            ThrowError("Payment file not found");
        }
        request.PaymentPath = await StorageService.UploadFileAsync(Files[0], "payments", ct);
        request.Status = RequestStatus.PaymentSubmitted;
        var history = new RequestHistory
        {
            RequestId = id,
            Remarks = "Payment submitted. Waiting for admin validation.",
            RequestStatus = RequestStatus.PaymentSubmitted,
        };
        await Db.RequestHistories.AddAsync(history, ct);
        await Db.SaveChangesAsync(ct);
        SendEmailNotification(request.Email, request.ReferenceNumber);
    }

    private void SendEmailNotification(string emailAddress, string referenceNumber)
    {
        var subject = $"Payment Submitted for Request Reference Number: {referenceNumber}";
        var body =
            @$"
            <p>Request Reference Number: {referenceNumber}</p>
            <p>Payment has been submitted.</p>
            <p>Please wait for admin to verify your payment another notification will be sent.</p>
            <p>Thank you.</p>
            ";
        EmailService.SendEmail(emailAddress, subject, body, isHtml: true);
    }
}
