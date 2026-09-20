using Ordering.Application.Abstractions;
using MediatR;

namespace Ordering.Application.Orders.ListCustomerOrders;

public sealed class ListCustomerOrdersQueryHandler(ICustomerOrderRepository repository)
    : IRequestHandler<ListCustomerOrdersQuery, IReadOnlyList<CustomerOrderDto>>
{
    public async Task<IReadOnlyList<CustomerOrderDto>> Handle(
        ListCustomerOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await repository.ListAsync(cancellationToken);

        return orders
            .Select(o => new CustomerOrderDto(
                o.Id,
                o.CustomerId,
                o.CustomerName,
                o.OfferId,
                o.OfferName,
                o.MonthlyFee,
                o.Status))
            .ToList();
    }
}
