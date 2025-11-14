using DbSetup.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbSetup.Data.Configurations;

public class SystemLogConfiguration : IEntityTypeConfiguration<SystemLog>
{
    public void Configure(EntityTypeBuilder<SystemLog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CorrelationId)
            .IsRequired();

        builder.Property(x => x.EventType)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.EventDescription)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.IpAddress)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.AdditionalData)
            .HasColumnType("text");

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}
