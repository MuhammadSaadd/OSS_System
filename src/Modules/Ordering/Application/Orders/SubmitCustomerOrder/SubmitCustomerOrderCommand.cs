using MediatR;
using SharedKernel;

namespace Ordering.Application.Orders.SubmitCustomerOrder;

public sealed record SubmitCustomerOrderCommand(Guid OrderId) : IRequest<Result>;
