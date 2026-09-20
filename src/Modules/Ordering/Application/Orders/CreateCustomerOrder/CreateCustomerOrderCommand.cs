using MediatR;
using SharedKernel;

namespace Ordering.Application.Orders.CreateCustomerOrder;

public sealed record CreateCustomerOrderCommand(
    Guid CustomerId,
    Guid OfferId) : IRequest<Result<Guid>>;
