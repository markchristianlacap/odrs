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
            .RequestDocuments.OrderByDescending(x => x.Request.CreatedAt)
            .Select(x => new RequestRecentRes
            {
                ReferenceNumber = x.Request.ReferenceNumber,
                FirstName = x.Request.FirstName,
                YearLevel = x.Request.YearLevel,
                Id = x.Request.Id,
                Type = x.Type,
                LastName = x.Request.LastName,
                Semester = x.Request.Semester,
                MiddleName = x.Request.MiddleName,
                ExtensionName = x.Request.ExtensionName,
                RequesterType = x.Request.RequesterType,
            })
            .Take(10)
            .ToListAsync(ct);
        Response = res;
    }
}
