using Inventory.Domain;

namespace Inventory.Application.Abstractions;

public interface IAccessPortRepository
{
    Task AddAsync(AccessPort port, CancellationToken cancellationToken = default);

    Task<AccessPort?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AccessPort>> ListAsync(
        AccessPortStatus? status = null,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(string exchange, string portLabel, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
