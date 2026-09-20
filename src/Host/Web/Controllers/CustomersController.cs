using Crm.Application.Customers.CreateCustomer;
using Crm.Application.Customers.GetCustomer;
using Crm.Application.Customers.ListCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class CustomersController(IMediator mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var customers = await mediator.Send(new ListCustomersQuery(), cancellationToken);
        return View(customers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateCustomerViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCustomerViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await mediator.Send(
            new CreateCustomerCommand(model.Name, model.Email, model.Address),
            cancellationToken);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return View(model);
        }

        TempData["Success"] = "Customer created.";
        return RedirectToAction(nameof(Details), new { id = result.Value });
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
    {
        var customer = await mediator.Send(new GetCustomerQuery(id), cancellationToken);

        if (customer is null)
            return NotFound();

        return View(customer);
    }
}
