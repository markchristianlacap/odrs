using Backend.Database;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.ChangePassword;

public class Endpoint : Endpoint<ChangePasswordReq>
{
    public AppDbContext Db { get; set; } = null!;
    public IUserService UserService { get; set; } = null!;

    public override void Configure()
    {
        Post("/user/change-password");
    }

    public override async Task HandleAsync(ChangePasswordReq req, CancellationToken ct)
    {
        var user = await Db.Users.FirstOrDefaultAsync(x => x.Id == UserService.UserId, ct);
        if (user == null)
        {
            ThrowError(x => x.CurrentPassword, "Incorrect password");
        }
        if (!BCrypt.Net.BCrypt.EnhancedVerify(req.CurrentPassword, user.Password))
        {
            ThrowError(x => x.CurrentPassword, "Incorrect password");
        }
        var hashed = BCrypt.Net.BCrypt.EnhancedHashPassword(req.NewPassword);
        user.Password = hashed;
        await Db.SaveChangesAsync(ct);
    }
}
