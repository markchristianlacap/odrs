using Backend.Database;
using Backend.Entities;
using Mapster;

namespace Backend.Features.User.Requests.Index;

public class Endpoint : Endpoint<RequestPagedReq, PagedRes<RequestRowRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/user/requests");
    }

    public override async Task HandleAsync(RequestPagedReq req, CancellationToken ct)
    {
        var query = Db.Requests.AsQueryable();
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            query = query.Where(x =>
                x.ReferenceNumber.Contains(req.Search)
                || x.Email.Contains(req.Search)
                || x.FirstName.Contains(req.Search)
                || x.LastName.Contains(req.Search)
            );
        }
        if (req.Status is not null)
        {
            query = query.Where(x => x.Status == req.Status);
        }
        if (req.DocumentType is not null)
        {
            query = query.Where(x => x.Documents.Any(d => d.Type == req.DocumentType));
        }
        var mapCfg = new TypeAdapterConfig();
        mapCfg
            .NewConfig<Request, RequestRowRes>()
            .Map(dest => dest.ProgramName, src => src.Program.Name)
            .Map(
                dest => dest.DocumentsDesc,
                src => string.Join(", ", src.Documents.Select(d => d.Type.ToString()))
            )
            .Map(dest => dest.CampusName, src => src.Campus.Name);
        var res = await query
            .OrderByDescending(x => x.CreatedAt)
            .ProjectToType<RequestRowRes>(mapCfg)
            .ToPagedAsync(req, ct);
        Response = res;
    }
}
