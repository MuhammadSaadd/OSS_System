using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

internal sealed class ProductOfferConfiguration : IEntityTypeConfiguration<ProductOffer>
{
    public void Configure(EntityTypeBuilder<ProductOffer> builder)
    {
        builder.ToTable("product_offers");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(o => o.Name)
            .IsUnique();

        builder.Property(o => o.DownloadMbps)
            .IsRequired();

        builder.Property(o => o.UploadMbps)
            .IsRequired();

        builder.Property(o => o.MonthlyFee)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(o => o.IsActive)
            .IsRequired();

        builder.Ignore(o => o.DomainEvents);
    }
}
