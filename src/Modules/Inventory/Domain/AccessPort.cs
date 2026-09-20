using SharedKernel;

namespace Inventory.Domain;

public sealed class AccessPort : AggregateRoot
{
    private AccessPort()
    {
    }

    public string Exchange { get; private set; } = string.Empty;

    public string PortLabel { get; private set; } = string.Empty;

    public AccessPortStatus Status { get; private set; }

    public static Result<AccessPort> Create(string exchange, string portLabel)
    {
        if (string.IsNullOrWhiteSpace(exchange))
            return Result.Failure<AccessPort>("Exchange is required.");

        if (string.IsNullOrWhiteSpace(portLabel))
            return Result.Failure<AccessPort>("Port label is required.");

        var port = new AccessPort
        {
            Exchange = exchange.Trim(),
            PortLabel = portLabel.Trim(),
            Status = AccessPortStatus.Available
        };

        return Result.Success(port);
    }

    public Result Reserve()
    {
        if (Status != AccessPortStatus.Available)
            return Result.Failure($"Port {Exchange}/{PortLabel} cannot be reserved from status {Status}.");

        Status = AccessPortStatus.Reserved;
        return Result.Success();
    }

    public Result Allocate()
    {
        if (Status == AccessPortStatus.Allocated)
            return Result.Failure($"Port {Exchange}/{PortLabel} is already allocated.");

        if (Status != AccessPortStatus.Reserved)
            return Result.Failure($"Port {Exchange}/{PortLabel} must be reserved before allocation.");

        Status = AccessPortStatus.Allocated;
        return Result.Success();
    }
}
