using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence;

public static class InventorySeed
{
    private const string Exchange = "EX-01";
    private const int PortCount = 12;

    public static async Task EnsureSeedDataAsync(
        InventoryDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        var exists = await dbContext.AccessPorts.AnyAsync(cancellationToken);

        if (exists)
            return;

        for (var i = 1; i <= PortCount; i++)
        {
            var port = AccessPort.Create(Exchange, $"P-{i:D3}");

            if (port.IsFailure)
                throw new InvalidOperationException($"Failed to seed access port: {port.Error}");

            await dbContext.AccessPorts.AddAsync(port.Value, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
