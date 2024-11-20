﻿using Backend.Enums;

namespace Backend.Features.User.Users;

public class UserModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string? ExtensionName { get; set; }
    public string Email { get; set; } = null!;
    public string ContactNumber { get; set; } = null!;
    public DateOnly? Birthdate { get; set; }
    public string Address { get; set; } = null!;
    public Role? Role { get; set; } = null;
}
