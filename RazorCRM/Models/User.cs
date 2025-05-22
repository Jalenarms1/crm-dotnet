using System;
using System.Text.Json.Serialization;

namespace RazorCRM.Models;

public class User
{
    public Guid Id = Guid.NewGuid();
    public string Name;
    
    public string Email;

    [JsonIgnore]
    public string Password;
}
