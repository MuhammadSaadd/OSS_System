using Inventory.Application.Abstractions;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

internal sealed class AccessPortRepository(InventoryDbContext dbContext) : IAccessPortRepository
{
    public async Task AddAsync(AccessPort port, CancellationToken cancellationToken = default)
    {
        await dbContext.AccessPorts.AddAsync(port, cancellationToken);
    }

    public async Task<AccessPort?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.AccessPorts
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AccessPort>> ListAsync(
        AccessPortStatus? status = null,
        CancellationToken cancellationToken = default)
    {
        var query = dbContext.AccessPorts.AsNoTracking();

        if (status is not null)
            query = query.Where(p => p.Status == status.Value);

        return await query
            .OrderBy(p => p.Exchange)
            .ThenBy(p => p.PortLabel)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        string exchange,
        string portLabel,
        CancellationToken cancellationToken = default)
    {
        var normalizedExchange = exchange.Trim();
        var normalizedPortLabel = portLabel.Trim();

        return await dbContext.AccessPorts
            .AnyAsync(p => p.Exchange == normalizedExchange && p.PortLabel == normalizedPortLabel,
                cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
