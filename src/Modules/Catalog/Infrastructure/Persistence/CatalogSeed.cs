using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence;

public static class CatalogSeed
{
    public const string Fiber100Name = "Fiber 100";

    public static async Task EnsureSeedDataAsync(
        CatalogDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        var exists = await dbContext.ProductOffers
            .AnyAsync(o => o.Name == Fiber100Name, cancellationToken);

        if (exists)
            return;

        var offer = ProductOffer.Create(
            name: Fiber100Name,
            downloadMbps: 100,
            uploadMbps: 20,
            monthlyFee: 49.99m);

        if (offer.IsFailure)
            throw new InvalidOperationException($"Failed to seed Fiber 100: {offer.Error}");

        await dbContext.ProductOffers.AddAsync(offer.Value, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
