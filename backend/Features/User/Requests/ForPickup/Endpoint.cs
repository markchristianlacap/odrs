using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.ForPickup;

public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/user/requests/{id:guid}/for-pickup");
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
        request.Status = RequestStatus.PendingForPickup;
        var status = new RequestHistory
        {
            RequestStatus = RequestStatus.PendingForPickup,
            Remarks =
                "Request is ready to pick up please go to the registrars office and pick up the request. Thank you.",
            RequestId = id,
        };
        await Db.RequestHistories.AddAsync(status, ct);
        await Db.SaveChangesAsync(ct);
    }
}
