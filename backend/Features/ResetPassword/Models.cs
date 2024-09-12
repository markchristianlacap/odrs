namespace Backend.Features.ResetPassword;

public class ResetPasswordReq
{
    public string Token { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}
