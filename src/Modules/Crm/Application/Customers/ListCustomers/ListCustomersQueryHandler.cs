using Crm.Application.Abstractions;
using MediatR;

namespace Crm.Application.Customers.ListCustomers;

public sealed class ListCustomersQueryHandler(ICustomerRepository repository)
    : IRequestHandler<ListCustomersQuery, IReadOnlyList<CustomerDto>>
{
    public async Task<IReadOnlyList<CustomerDto>> Handle(
        ListCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await repository.ListAsync(cancellationToken);

        return customers
            .Select(c => new CustomerDto(
                c.Id,
                c.Name,
                c.Email,
                c.Address,
                c.Status))
            .ToList();
    }
}
