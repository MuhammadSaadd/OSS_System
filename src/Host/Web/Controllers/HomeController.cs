using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Web.Models;
using Web.Persistence;

namespace Web.Controllers;

public class HomeController(
    HostDbContext dbContext,
    HealthCheckService healthCheckService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);
        var report = await healthCheckService.CheckHealthAsync(cancellationToken);

        var model = new HomeViewModel
        {
            Title = "Telecom Demo",
            DatabaseConnected = canConnect && report.Status == HealthStatus.Healthy,
            DatabaseStatus = canConnect
                ? report.Status.ToString()
                : "Unreachable"
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
