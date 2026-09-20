using Inventory.Application.Abstractions;
using MediatR;

namespace Inventory.Application.Ports.ListAccessPorts;

public sealed class ListAccessPortsQueryHandler(IAccessPortRepository repository)
    : IRequestHandler<ListAccessPortsQuery, IReadOnlyList<AccessPortDto>>
{
    public async Task<IReadOnlyList<AccessPortDto>> Handle(
        ListAccessPortsQuery request,
        CancellationToken cancellationToken)
    {
        var ports = await repository.ListAsync(request.Status, cancellationToken);

        return ports
            .Select(p => new AccessPortDto(
                p.Id,
                p.Exchange,
                p.PortLabel,
                p.Status))
            .ToList();
    }
}
