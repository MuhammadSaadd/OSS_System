using MediatR;

namespace Crm.Application.Customers.ListCustomers;

public sealed record ListCustomersQuery : IRequest<IReadOnlyList<CustomerDto>>;
