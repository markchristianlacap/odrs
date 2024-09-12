using Backend.Database;
using Backend.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.UpdateProfile;

public class Endpoint : Endpoint<UpdateProfileReq>
{
    public AppDbContext Db { get; set; } = null!;
    public IUserService UserService { get; set; } = null!;

    public override void Configure()
    {
        Post("/user/update-profile");
    }

    public override async Task HandleAsync(UpdateProfileReq req, CancellationToken ct)
    {
        var user = await Db.Users.FirstOrDefaultAsync(x => x.Id == UserService.UserId, ct);
        if (user == null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        req.Adapt(user);
        await Db.SaveChangesAsync(ct);
    }
}
