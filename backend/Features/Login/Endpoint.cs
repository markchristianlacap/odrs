using System.Security.Claims;
using Backend.Database;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Login;

public class Endpoint : Endpoint<LoginReq>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginReq req, CancellationToken ct)
    {
        var user = await Db
            .Users.Where(x => x.Email == req.Email)
            .Select(x => new
            {
                x.Password,
                x.Id,
                x.Role,
            })
            .FirstOrDefaultAsync(ct);
        if (user == null)
        {
            ThrowError("Incorrect email or password");
        }
        var incorrectPassword = !BCrypt.Net.BCrypt.EnhancedVerify(req.Password, user.Password);
        if (incorrectPassword || user == null)
        {
            ThrowError("Incorrect email or password");
        }
        await CookieAuth.SignInAsync(u =>
        {
            u.Claims.Add(new(ClaimTypes.NameIdentifier, user.Id.ToString()));
            u.Roles.Add(user.Role.ToString());
        });
    }
}
