using System;
using System.Text.Json.Serialization;
using CRMApi.Enums;

namespace CRMApi.Models;

public class User
{
   

    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    public UserRoleEnum? Role { get; set; } = UserRoleEnum.User;
}
