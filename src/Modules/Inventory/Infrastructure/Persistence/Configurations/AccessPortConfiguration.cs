using Inventory.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

internal sealed class AccessPortConfiguration : IEntityTypeConfiguration<AccessPort>
{
    public void Configure(EntityTypeBuilder<AccessPort> builder)
    {
        builder.ToTable("access_ports");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Exchange)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.PortLabel)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(p => new { p.Exchange, p.PortLabel })
            .IsUnique();

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Ignore(p => p.DomainEvents);
    }
}
