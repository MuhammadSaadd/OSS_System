using Inventory.Domain;
using MediatR;

namespace Inventory.Application.Ports.ListAccessPorts;

public sealed record ListAccessPortsQuery(AccessPortStatus? Status = null)
    : IRequest<IReadOnlyList<AccessPortDto>>;
