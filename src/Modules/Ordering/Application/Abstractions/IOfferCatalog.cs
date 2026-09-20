namespace Ordering.Application.Abstractions;

/// <summary>
/// Read port fulfilled by a host-registered adapter over Catalog queries.
/// Keeps Ordering decoupled from the Catalog module.
/// </summary>
public interface IOfferCatalog
{
    Task<OfferSnapshot?> FindByIdAsync(Guid offerId, CancellationToken cancellationToken = default);
}
