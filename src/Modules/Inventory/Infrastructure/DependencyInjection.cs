using Inventory.Application;
using Inventory.Application.Abstractions;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInventoryModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInventoryApplication();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        services.AddDbContext<InventoryDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IAccessPortRepository, AccessPortRepository>();

        return services;
    }

    public static async Task ApplyInventoryMigrationsAsync(
        this IServiceProvider services,
        CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
        await InventorySeed.EnsureSeedDataAsync(dbContext, cancellationToken);
    }
}
