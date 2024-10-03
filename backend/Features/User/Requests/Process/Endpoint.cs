using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.Process;

public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;

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
    }
}
