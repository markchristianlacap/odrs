namespace Backend.Features.User.Users.Store;

public class UserStoreReq : UserModel
{
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}
