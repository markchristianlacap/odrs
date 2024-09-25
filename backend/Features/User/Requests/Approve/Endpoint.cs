using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.Approve;

public class Endpoint : Endpoint<ApproveRequestReq>
{
    public AppDbContext Db { get; set; } = null!;

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
        request.Amount = decimal.Parse(req.Amount);
        var status = new RequestHistory
        {
            RequestStatus = RequestStatus.WaitingForPayment,
            Remarks = "Request Approved",
            RequestId = id,
        };
        await Db.RequestHistories.AddAsync(status, ct);
        await Db.SaveChangesAsync(ct);
    }
}
