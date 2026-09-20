using Ordering.Application.Abstractions;
using MediatR;

namespace Ordering.Application.Orders.GetCustomerOrder;

public sealed class GetCustomerOrderQueryHandler(ICustomerOrderRepository repository)
    : IRequestHandler<GetCustomerOrderQuery, CustomerOrderDto?>
{
    public async Task<CustomerOrderDto?> Handle(
        GetCustomerOrderQuery request,
        CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (order is null)
            return null;

        return new CustomerOrderDto(
            order.Id,
            order.CustomerId,
            order.CustomerName,
            order.OfferId,
            order.OfferName,
            order.MonthlyFee,
            order.Status);
    }
}
