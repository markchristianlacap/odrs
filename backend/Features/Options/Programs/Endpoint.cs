using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Options.Programs;

public class Endpoint : Endpoint<ProgramOptionsReq, List<ProgramOptionsRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/options/programs");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ProgramOptionsReq req, CancellationToken ct)
    {
        var query = Db.Programs.AsQueryable();
        if (req.CampusId is not null)
        {
            query = query.Where(x => x.CampusId == req.CampusId);
        }
        var res = await query.ProjectToType<ProgramOptionsRes>().ToListAsync(ct);
        Response = res;
    }
}
