using Inventory.Domain;

namespace Inventory.Application.Ports;

public sealed record AccessPortDto(
    Guid Id,
    string Exchange,
    string PortLabel,
    AccessPortStatus Status);
