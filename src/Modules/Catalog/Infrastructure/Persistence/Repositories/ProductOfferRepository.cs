using Catalog.Application.Abstractions;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Repositories;

internal sealed class ProductOfferRepository(CatalogDbContext dbContext) : IProductOfferRepository
{
    public async Task AddAsync(ProductOffer offer, CancellationToken cancellationToken = default)
    {
        await dbContext.ProductOffers.AddAsync(offer, cancellationToken);
    }

    public async Task<ProductOffer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.ProductOffers
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductOffer>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.ProductOffers
            .OrderBy(o => o.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var normalized = name.Trim();
        return await dbContext.ProductOffers
            .AnyAsync(o => o.Name == normalized, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
