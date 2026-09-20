using Ordering.Domain;

namespace Ordering.Application.Abstractions;

public interface ICustomerOrderRepository
{
    Task AddAsync(CustomerOrder order, CancellationToken cancellationToken = default);

    Task<CustomerOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CustomerOrder>> ListAsync(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
