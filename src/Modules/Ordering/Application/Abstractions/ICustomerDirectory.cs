namespace Ordering.Application.Abstractions;

/// <summary>
/// Read port fulfilled by a host-registered adapter over Crm queries.
/// Keeps Ordering decoupled from the Crm module.
/// </summary>
public interface ICustomerDirectory
{
    Task<CustomerSnapshot?> FindByIdAsync(Guid customerId, CancellationToken cancellationToken = default);
}
