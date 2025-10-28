using DbSetup.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbSetup.Data.Configurations;

public class BankConfiguration : IEntityTypeConfiguration<ReferenceBank>
{
    public void Configure(EntityTypeBuilder<ReferenceBank> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.BankName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.BankCode)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Country)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}