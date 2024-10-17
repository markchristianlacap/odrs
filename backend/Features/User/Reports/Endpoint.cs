using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Reports;

public class Endpoint : Endpoint<ReportReq, List<ReportRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/user/reports");
    }

    public override async Task HandleAsync(ReportReq req, CancellationToken ct)
    {
        var query = Db.Requests.AsQueryable();
        if (req.DateTo is not null)
        {
            query = query.Where(x => x.CreatedAt.Date <= req.DateTo.Value.Date);
        }

        if (req.DateFrom is not null)
        {
            query = query.Where(x => x.CreatedAt.Date >= req.DateFrom.Value.Date);
        }

        if (req.Status is not null)
        {
            query = query.Where(x => x.Status == req.Status);
        }
        Response = await query.ProjectToType<ReportRes>().ToListAsync(ct);
    }
}
