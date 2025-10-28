namespace DbSetup.Data.Entities;

public class ReferenceBank : IAuditableEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public string BankName { get; set; } = string.Empty;
    public string BankCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}