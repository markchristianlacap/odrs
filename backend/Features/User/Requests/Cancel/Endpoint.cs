using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.Cancel;

public class Endpoint : Endpoint<CancelRequestReq>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/user/requests/{id:guid}/cancel");
    }

    public override async Task HandleAsync(CancelRequestReq req, CancellationToken ct)
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
            RequestStatus = RequestStatus.Canceled,
        };
        request.Status = RequestStatus.Canceled;
        await Db.RequestHistories.AddAsync(status, ct);
        await Db.SaveChangesAsync(ct);
    }
}
