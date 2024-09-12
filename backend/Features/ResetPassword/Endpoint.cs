using Backend.Database;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.ResetPassword;

public class Endpoint : Endpoint<ResetPasswordReq>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/reset-password");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ResetPasswordReq req, CancellationToken ct)
    {
        var userId = await Db
            .ResetPasswords.Where(x => x.Token == req.Token)
            .Select(x => x.UserId)
            .FirstOrDefaultAsync(ct);
        if (userId == default)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        var user = await Db.Users.Where(x => x.Id == userId).FirstOrDefaultAsync(ct);
        if (user == null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        var trans = await Db.Database.BeginTransactionAsync(ct);
        user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(req.Password);
        await Db.ResetPasswords.Where(x => x.Token == req.Token).ExecuteDeleteAsync(ct);
        await Db.SaveChangesAsync(ct);
        await trans.CommitAsync(ct);
    }
}
