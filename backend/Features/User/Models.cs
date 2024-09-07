using Backend.Enums;

namespace Backend.Features.User;

public class UserRes
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;
  public string Email { get; set; } = null!;
  public Role Role { get; set; }
  public string RoleDesc => Role.ToString();
}
