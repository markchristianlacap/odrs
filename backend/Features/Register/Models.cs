namespace Backend.Features.Register;

public class RegisterReq
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ContactNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}
