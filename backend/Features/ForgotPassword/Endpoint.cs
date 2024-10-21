using Backend.Database;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using ResetPasswordEntity = Backend.Entities.ResetPassword;

namespace Backend.Features.ForgotPassword;

public class Endpoint : Endpoint<ForgotPasswordReq>
{
    public IUserService UserService { get; set; } = null!;
    public AppDbContext Db { get; set; } = null!;
    public IEmailService EmailService { get; set; } = null!;

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
        await SendResetLink(req.Email);
    }

    public async Task<bool> SendResetLink(string emailAddress)
    {
        var userId = await Db
            .Users.Where(x => x.Email == emailAddress)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        if (userId == Guid.Empty)
        {
            return false;
        }

        var token = await GenerateToken();
        var resetPassword = new ResetPasswordEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow,
        };

        await Db.ResetPasswords.AddAsync(resetPassword);
        await Db.SaveChangesAsync();
        var subject = "Reset Password Link";
        var body =
            @$"
            <p>Hello,</p>
            <p>You requested a password reset.</p>
            <p>Please follow the link below to reset your password.</p>
            <br/>
            Click <a href='{BaseURL}reset-password?token={token}'>here</a> to reset your password.
            ";
        return await EmailService.SendEmail(emailAddress, subject, body, true);
    }

    private async Task<string> GenerateToken()
    {
        var token = Guid.NewGuid().ToString();
        var isTaken = await Db.ResetPasswords.AnyAsync(x => x.Token == token);
        if (isTaken)
        {
            return await GenerateToken();
        }
        return token;
    }
}
