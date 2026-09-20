using Catalog.Infrastructure;
using Crm.Infrastructure;
using Inventory.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Abstractions;
using Ordering.Infrastructure;
using Web.Integration;
using Web.Persistence;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

builder.Services.AddDbContext<HostDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddCrmModule(builder.Configuration);
builder.Services.AddInventoryModule(builder.Configuration);
builder.Services.AddOrderingModule(builder.Configuration);

builder.Services.AddScoped<ICustomerDirectory, CustomerDirectory>();
builder.Services.AddScoped<IOfferCatalog, OfferCatalog>();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<HostDbContext>("postgres");

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.Services.ApplyCatalogMigrationsAsync();
    await app.Services.ApplyCrmMigrationsAsync();
    await app.Services.ApplyInventoryMigrationsAsync();
    await app.Services.ApplyOrderingMigrationsAsync();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapHealthChecks("/health");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
