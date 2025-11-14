using DbSetup.Data.Enums;

namespace DbSetup.Data.Entities;

public sealed class Account
{
    public Guid AccountId { get; set; }
    public Guid BankId { get; set; }

    public required string AccountName { get; set; } 
    public required string AccountNumber { get; set; } 
    public required string RoutingNumber { get; set; } 

    public AccountStatus Status { get; set; } 

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
