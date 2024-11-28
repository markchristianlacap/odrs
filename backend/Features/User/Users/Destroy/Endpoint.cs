using Backend.Database;
using Backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Users.Destroy;

public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Delete("/user/users/{id:guid}");
        Roles(nameof(Role.Admin));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var user = await Db.Users.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        user.DeletedAt = DateTime.UtcNow;
        await Db.SaveChangesAsync(ct);
    }
}
