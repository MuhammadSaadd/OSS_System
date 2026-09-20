using Catalog.Domain;

namespace Catalog.Application.Abstractions;

public interface IProductOfferRepository
{
    Task AddAsync(ProductOffer offer, CancellationToken cancellationToken = default);

    Task<ProductOffer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProductOffer>> ListAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
