using Inventory.Application.Ports.ListAccessPorts;
using Inventory.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class PortsController(IMediator mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(
        AccessPortStatus? status,
        CancellationToken cancellationToken)
    {
        var ports = await mediator.Send(new ListAccessPortsQuery(status), cancellationToken);

        ViewBag.Status = status;
        return View(ports);
    }
}
