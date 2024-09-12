namespace Backend.Features.Login;

public class LoginReq
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool Remember { get; set; }
}
