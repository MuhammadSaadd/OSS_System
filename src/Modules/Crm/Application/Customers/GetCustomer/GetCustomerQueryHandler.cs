using Crm.Application.Abstractions;
using MediatR;

namespace Crm.Application.Customers.GetCustomer;

public sealed class GetCustomerQueryHandler(ICustomerRepository repository)
    : IRequestHandler<GetCustomerQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(
        GetCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (customer is null)
            return null;

        return new CustomerDto(
            customer.Id,
            customer.Name,
            customer.Email,
            customer.Address,
            customer.Status);
    }
}
