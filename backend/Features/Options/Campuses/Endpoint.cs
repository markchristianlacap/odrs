using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Options.Campuses;
public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;
    public override void Configure()
    {
        Get("/options/campuses");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await Db
            .Campuses
            .ProjectToType<CampusOptionRes>()
            .ToListAsync(ct);
        Response = res;
    }
}

