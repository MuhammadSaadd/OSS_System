using MediatR;
using SharedKernel;

namespace Crm.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(
    string Name,
    string Email,
    string Address) : IRequest<Result<Guid>>;
