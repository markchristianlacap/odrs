using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Configurations.Index;

public class Endpoint : EndpointWithoutRequest<List<ConfigurationRowRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/user/configurations");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var configurations = await Db
            .Configurations.ProjectToType<ConfigurationRowRes>()
            .ToListAsync(ct);
        Response = configurations;
    }
}
