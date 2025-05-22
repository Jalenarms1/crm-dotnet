using System;
using System.Text.Json.Serialization;

namespace CRMApi.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Password { get; set; }
    
    [JsonPropertyName("password")]
    private string PasswordSetter
    {
        set { Password = value; }
    }
}
