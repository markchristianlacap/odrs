using Backend.Database;
using Backend.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;
using UserEntity = Backend.Entities.User;

namespace Backend.Features.Register;

public class Endpoint : Endpoint<RegisterReq>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterReq req, CancellationToken ct)
    {
        var emailExist = await Db.Users.AnyAsync(x => x.Email == req.Email, ct);
        if (emailExist)
        {
            ThrowError(x => x.Email, "Email already exists");
        }
        var user = req.Adapt<UserEntity>();
        user.Role = Role.Requester;
        user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(req.Password);
        await Db.Users.AddAsync(user, ct);
        await Db.SaveChangesAsync(ct);
    }
}
