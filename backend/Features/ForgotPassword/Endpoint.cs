using Backend.Database;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.ForgotPassword;

public class Endpoint : Endpoint<ForgotPasswordReq>
{
    public IUserService UserService { get; set; } = null!;
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/forgot-password");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ForgotPasswordReq req, CancellationToken ct)
    {
        var validEmail = await Db.Users.AnyAsync(x => x.Email == req.Email, ct);
        if (!validEmail)
        {
            ThrowError(x => x.Email, "Sorry, we don't have an account with that email address");
        }
        await UserService.SendResetLink(req.Email);
    }
}
