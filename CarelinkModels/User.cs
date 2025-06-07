using System;
using System.Text.Json.Serialization;

namespace CarelinkModels;

public class User
{


    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    public UserRoleEnum? Role { get; set; } = UserRoleEnum.User;
}

public enum UserRoleEnum
{
    User,
    Admin,
    StaffOne,
    StaffTwo,
    StaffThree
}

