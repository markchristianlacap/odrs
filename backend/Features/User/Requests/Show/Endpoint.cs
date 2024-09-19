using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.Show;

public class Endpoint : EndpointWithoutRequest<RequestShowRes>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/user/requests/{id:guid}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var request = await Db
            .Requests.AsQueryable()
            .Where(x => x.Id == id)
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
