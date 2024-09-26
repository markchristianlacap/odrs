using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.Reject;

public class Endpoint : Endpoint<RejectRequestReq>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/user/requests/{id:guid}/reject");
    }

    public override async Task HandleAsync(RejectRequestReq req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var request = await Db.Requests.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (request is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        var status = new RequestHistory
        {
            RequestId = id,
            Remarks = req.Remarks,
            RequestStatus = RequestStatus.Rejected,
        };
        request.Status = RequestStatus.Rejected;
        await Db.RequestHistories.AddAsync(status, ct);
        await Db.SaveChangesAsync(ct);
    }
}
