using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.Recent;

public class Endpoint : EndpointWithoutRequest<List<RequestRecentRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/user/requests/recent");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await Db
            .Requests.OrderByDescending(x => x.CreatedAt)
            .ProjectToType<RequestRecentRes>()
            .Take(10)
            .ToListAsync(ct);
        Response = res;
    }
}
