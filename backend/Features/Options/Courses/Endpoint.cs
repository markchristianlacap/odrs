using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Options.Courses;
public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;
    public override void Configure()
    {
        Get("/options/courses");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await Db
            .Courses
            .ProjectToType<CourseOptionRes>()
            .ToListAsync(ct);
        Response = res;
    }
}

