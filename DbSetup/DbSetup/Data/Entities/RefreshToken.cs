namespace DbSetup.Data.Entities;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid UserId { get; set; }
    public DateTime ExpiresOnUtc { get; set; }

    public required string Value { get; set; }
    public required string Jti { get; set; }
}