using MediatR;

namespace Crm.Application.Customers.GetCustomer;

public sealed record GetCustomerQuery(Guid Id) : IRequest<CustomerDto?>;
