namespace CarelinkModels;

public class Patient
{
    public Guid Id { get; set; } = Guid.NewGuid();
     public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty; 

    public string? SSN { get; set; }
    public string MedicalRecordNumber { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

}
