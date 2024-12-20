﻿using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.ForRelease;

public class Endpoint : Endpoint<ReleaseDocumentsReq>
{
    public AppDbContext Db { get; set; } = null!;
    public IEmailService EmailService { get; set; } = null!;

    public override void Configure()
    {
        Post("/user/requests/{id:guid}/for-release");
    }

    public override async Task HandleAsync(ReleaseDocumentsReq req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var request = await Db.Requests.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (request is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        request.Status = RequestStatus.PendingForRelease;
        request.ClaimDeadline = req.ClaimDeadline;
        var status = new RequestHistory
        {
            RequestStatus = RequestStatus.PendingForRelease,
            Remarks =
                $"Request is ready to pick up please go to the registrars office and pick up the request. Make sure to pick up the request before or on ${req.ClaimDeadline:MMM/dd/yyyy} Thank you.",
            RequestId = id,
        };
        await Db.RequestHistories.AddAsync(status, ct);
        await Db.SaveChangesAsync(ct);
        SendEmailNotification(request.Email, request.ReferenceNumber);
    }

    private void SendEmailNotification(string emailAddress, string referenceNumber)
    {
        var subject =
            "Your Request with Reference Number: " + referenceNumber + " is Ready For Pick Up";
        var body =
            @$"
            <p>Request is ready to pick up. Please go to the registrars office and pick up the request.</p>
            <p>Thank you.</p>
            ";
        EmailService.SendEmail(emailAddress, subject, body, isHtml: true);
    }
}
