using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.ReleaseDocuments;

public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;

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
        if (request.Status != RequestStatus.PendingForPickup)
        {
            await SendForbiddenAsync(ct);
            return;
        }
        request.Status = RequestStatus.Released;
        request.DateOfReleased = DateTime.Now;
        var history = new RequestHistory
        {
            RequestId = id,
            Remarks = "Request released",
            RequestStatus = RequestStatus.Released,
        };
        await Db.RequestHistories.AddAsync(history, ct);
        await Db.SaveChangesAsync(ct);
    }
}
