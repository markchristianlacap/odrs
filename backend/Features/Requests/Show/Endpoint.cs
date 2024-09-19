using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Requests.Show;

public class Endpoint : EndpointWithoutRequest<RequestShowRes>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/requests/{referenceNumber}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var referenceNumber = Route<string>("referenceNumber");
        var request = await Db
            .Requests.AsQueryable()
            .Where(x => x.ReferenceNumber == referenceNumber)
            .ProjectToType<RequestShowRes>()
            .FirstOrDefaultAsync(ct);
        if (request == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        Response = request;
    }
}
