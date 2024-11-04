using Backend.Database;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Configurations.Store;

public class Endpoint : Endpoint<ConfigurationStoreReq>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/user/configurations");
    }

    public override async Task HandleAsync(ConfigurationStoreReq req, CancellationToken ct)
    {
        foreach (var configuration in req.Configurations)
        {
            var existing = await Db
                .Configurations.Where(x => x.Property == configuration.Property)
                .FirstOrDefaultAsync(ct);
            if (existing != null)
            {
                existing.Value = configuration.Value;
            }
            else
            {
                Db.Configurations.Add(
                    new Configuration
                    {
                        Property = configuration.Property,
                        Value = configuration.Value,
                    }
                );
            }
        }
        await Db.SaveChangesAsync(ct);
    }
}
