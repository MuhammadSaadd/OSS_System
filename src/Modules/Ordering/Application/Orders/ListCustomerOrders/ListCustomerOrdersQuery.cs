using MediatR;

namespace Ordering.Application.Orders.ListCustomerOrders;

public sealed record ListCustomerOrdersQuery : IRequest<IReadOnlyList<CustomerOrderDto>>;
