using Backend.Entities.Common;
using Backend.Enums;

namespace Backend.Entities;

public class User : BaseEntity
{
  public string Name { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string ContactNumber { get; set; } = null!;
  public string Password { get; set; } = null!;
  public Role Role { get; set; }
  public bool IsActive { get; set; } = false;
}
