using Catalog.Application.Offers.GetProductOffer;
using Crm.Application.Customers.GetCustomer;
using MediatR;
using Ordering.Application.Abstractions;

namespace Web.Integration;

/// <summary>
/// Composition-root adapter satisfying Ordering's read ports by dispatching
/// in-process MediatR queries to Crm and Catalog — modules stay decoupled.
/// </summary>
public sealed class CustomerDirectory(IMediator mediator) : ICustomerDirectory
{
    public async Task<CustomerSnapshot?> FindByIdAsync(
        Guid customerId,
        CancellationToken cancellationToken = default)
    {
        var customer = await mediator.Send(new GetCustomerQuery(customerId), cancellationToken);

        return customer is null
            ? null
            : new CustomerSnapshot(customer.Id, customer.Name, customer.Status == Crm.Domain.CustomerStatus.Active);
    }
}

public sealed class OfferCatalog(IMediator mediator) : IOfferCatalog
{
    public async Task<OfferSnapshot?> FindByIdAsync(
        Guid offerId,
        CancellationToken cancellationToken = default)
    {
        var offer = await mediator.Send(new GetProductOfferQuery(offerId), cancellationToken);

        return offer is null
            ? null
            : new OfferSnapshot(offer.Id, offer.Name, offer.MonthlyFee, offer.IsActive);
    }
}
