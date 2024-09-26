using Backend.Database;
using Backend.Entities;
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
        var mapCfG = new TypeAdapterConfig();
        mapCfG
            .NewConfig<RequestHistory, HistoryModel>()
            .Map(d => d.CreatedBy, s => s.CreatedBy!.FirstName);

        var request = await Db
            .Requests.AsQueryable()
            .Where(x => x.Id == id)
            .ProjectToType<RequestShowRes>(mapCfG)
            .FirstOrDefaultAsync(ct);
        if (request == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        Response = request;
    }
}
