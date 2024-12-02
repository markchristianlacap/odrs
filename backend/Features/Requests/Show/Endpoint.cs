using Backend.Database;
using Backend.Entities;
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
        var cfg = new TypeAdapterConfig();
        cfg.ForType<Request, RequestShowRes>()
            .Map(
                d => d.Histories,
                s => s.Histories.Adapt<List<HistoryModel>>().OrderBy(x => x.CreatedAt)
            );
        var request = await Db
            .Requests.AsQueryable()
            .Where(x => x.ReferenceNumber == referenceNumber)
            .ProjectToType<RequestShowRes>(cfg)
            .FirstOrDefaultAsync(ct);
        if (request == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        Response = request;
    }
}
