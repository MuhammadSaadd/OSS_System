using Microsoft.EntityFrameworkCore;
using Ordering.Application.Abstractions;
using Ordering.Domain;

namespace Ordering.Infrastructure.Persistence.Repositories;

internal sealed class CustomerOrderRepository(OrderingDbContext dbContext) : ICustomerOrderRepository
{
    public async Task AddAsync(CustomerOrder order, CancellationToken cancellationToken = default)
    {
        await dbContext.CustomerOrders.AddAsync(order, cancellationToken);
    }

    public async Task<CustomerOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.CustomerOrders
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CustomerOrder>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.CustomerOrders
            .OrderBy(o => o.CustomerName)
            .ThenBy(o => o.OfferName)
            .ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
