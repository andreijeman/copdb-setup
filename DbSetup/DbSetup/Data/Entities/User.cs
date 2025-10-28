using Microsoft.AspNetCore.Identity;

namespace DbSetup.Data.Entities;

public class User : IdentityUser<Guid>, IAuditableEntity
{
    public Guid? BankId { get; set; }

    public required string UserStatus { get; set; }
    public string? Region { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
