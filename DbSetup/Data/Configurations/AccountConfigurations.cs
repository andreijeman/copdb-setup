using DbSetup.Data.Entities;
using DbSetup.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbSetup.Data.Configurations;

public sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.AccountId);

        builder.Property(a => a.AccountId)
            .ValueGeneratedNever();

        builder.Property(a => a.BankId)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(a => a.AccountName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.AccountNumber)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.RoutingNumber)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.Status)
            .HasMaxLength(50)
            .HasConversion(
                v => v.ToString().ToUpperInvariant(),
                v => Enum.Parse<AccountStatus>(v, true)
            )
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .IsRequired();

        builder.HasIndex(a => a.AccountName)
            .IsUnique();

        builder.HasIndex(a => a.AccountNumber)
            .IsUnique();

        builder.HasIndex(a => a.RoutingNumber)
            .IsUnique();
    }
}
