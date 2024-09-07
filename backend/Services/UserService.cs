using System.Security.Claims;
using Backend.Database;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public interface IUserService
{
    Guid? UserId { get; }
    Task<bool> SendResetLink(string email);
}

public class UserService(IHttpContextAccessor ctx, IEmailService emailService, AppDbContext db)
    : IUserService
{
    public Guid? UserId =>
        Guid.TryParse(ctx.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id)
            ? id
            : null;

    public async Task<bool> SendResetLink(string emailAddress)
    {
        var userId = await db
            .Users.Where(x => x.Email == emailAddress)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        if (userId == Guid.Empty)
        {
            return false;
        }

        var token = await GenerateToken();
        var resetPassword = new ResetPassword
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow,
        };

        await db.ResetPasswords.AddAsync(resetPassword);
        await db.SaveChangesAsync();
        var subject = "Reset Password";
        var body =
            $"Click <a href=\"http://localhost:3000/reset-password?token={token}\">here</a> to reset your password.";
        return await emailService.SendEmail(emailAddress, subject, body);
    }

    private async Task<string> GenerateToken()
    {
        var token = Guid.NewGuid().ToString();
        var isTaken = await db.ResetPasswords.AnyAsync(x => x.Token == token);
        if (isTaken)
        {
            return await GenerateToken();
        }
        return token;
    }
}
