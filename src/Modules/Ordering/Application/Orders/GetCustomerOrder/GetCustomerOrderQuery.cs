using MediatR;

namespace Ordering.Application.Orders.GetCustomerOrder;

public sealed record GetCustomerOrderQuery(Guid Id) : IRequest<CustomerOrderDto?>;
