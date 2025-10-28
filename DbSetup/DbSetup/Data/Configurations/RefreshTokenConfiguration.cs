using DbSetup.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbSetup.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Jti)
            .HasMaxLength(128);

        builder.Property(x => x.ExpiresOnUtc)
            .IsRequired();

        builder.HasIndex(x => x.Value)
            .IsUnique();

        builder.HasIndex(x => x.Jti);

        builder.HasIndex(x => new { x.UserId, x.ExpiresOnUtc });
    }
}