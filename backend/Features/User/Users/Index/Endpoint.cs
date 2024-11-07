using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Users.Index;

public class Endpoint : Endpoint<UserPagedReq, PagedRes<UserRowRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/user/users");
    }

    public override async Task HandleAsync(UserPagedReq req, CancellationToken ct)
    {
        var query = Db.Users.AsNoTracking();
        if (req.Search is not null)
        {
            query = query.Where(x =>
                x.FirstName.Contains(req.Search)
                || x.LastName.Contains(req.Search)
                || x.Email.Contains(req.Search)
            );
        }

        Response = await query.ProjectToType<UserRowRes>().ToPagedAsync(req, ct);
    }
}
