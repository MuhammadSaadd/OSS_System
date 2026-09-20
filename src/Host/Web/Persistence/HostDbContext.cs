using Microsoft.EntityFrameworkCore;

namespace Web.Persistence;

/// <summary>
/// Placeholder DbContext used by the host to verify PostgreSQL connectivity.
/// Module-specific DbContexts will be registered in later deliverables.
/// </summary>
public sealed class HostDbContext(DbContextOptions<HostDbContext> options) : DbContext(options);
