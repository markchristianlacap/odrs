using Backend.Database;
using Backend.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User;

public class Endpoint : EndpointWithoutRequest<UserRes>
{
    public IUserService UserService { get; set; } = null!;
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/user");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await Db
            .Users.ProjectToType<UserRes>()
            .FirstOrDefaultAsync(x => x.Id == UserService.UserId, ct);
        if (res == null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        Response = res;
    }
}
