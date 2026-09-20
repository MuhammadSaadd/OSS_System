using Crm.Domain;

namespace Crm.Application.Customers;

public sealed record CustomerDto(
    Guid Id,
    string Name,
    string Email,
    string Address,
    CustomerStatus Status);
