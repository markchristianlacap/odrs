namespace Backend.Features.User.ChangePassword;

public class ChangePasswordReq
{
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}
