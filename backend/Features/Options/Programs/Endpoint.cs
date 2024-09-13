using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Options.Programs;

public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/options/programs");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await Db.Programs.ProjectToType<ProgramOptionsRes>().ToListAsync(ct);
        Response = res;
    }
}
