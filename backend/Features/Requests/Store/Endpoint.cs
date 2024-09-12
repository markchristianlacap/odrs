using Backend.Database;
using Backend.Entities;
using Mapster;

namespace Backend.Features.Requests.Store;

public class Endpoint : Endpoint<RequestReq>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/requests");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RequestReq req, CancellationToken ct)
    {
        var request = req.Adapt<Request>();
        await Db.Requests.AddAsync(request, ct);
        await Db.SaveChangesAsync(ct);
    }
}
