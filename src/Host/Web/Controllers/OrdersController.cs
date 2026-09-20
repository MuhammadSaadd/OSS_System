using Catalog.Application.Offers.ListProductOffers;
using Crm.Application.Customers.ListCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ordering.Application.Orders.CreateCustomerOrder;
using Ordering.Application.Orders.GetCustomerOrder;
using Ordering.Application.Orders.ListCustomerOrders;
using Ordering.Application.Orders.SubmitCustomerOrder;
using Web.Models;

namespace Web.Controllers;

public class OrdersController(IMediator mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var orders = await mediator.Send(new ListCustomerOrdersQuery(), cancellationToken);
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        return View(await BuildCreateViewModel(cancellationToken));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOrderViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(await BuildCreateViewModel(cancellationToken, preserve: model));

        var result = await mediator.Send(
            new CreateCustomerOrderCommand(model.CustomerId, model.OfferId),
            cancellationToken);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return View(await BuildCreateViewModel(cancellationToken, preserve: model));
        }

        TempData["Success"] = "Order placed as Draft.";
        return RedirectToAction(nameof(Details), new { id = result.Value });
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
    {
        var order = await mediator.Send(new GetCustomerOrderQuery(id), cancellationToken);

        if (order is null)
            return NotFound();

        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new SubmitCustomerOrderCommand(id), cancellationToken);

        TempData[result.IsSuccess ? "Success" : "Error"] =
            result.IsSuccess ? "Order submitted." : result.Error;

        return RedirectToAction(nameof(Details), new { id });
    }

    private async Task<CreateOrderViewModel> BuildCreateViewModel(
        CancellationToken cancellationToken,
        CreateOrderViewModel? preserve = null)
    {
        var customers = await mediator.Send(new ListCustomersQuery(), cancellationToken);
        var offers = await mediator.Send(new ListProductOffersQuery(), cancellationToken);

        return new CreateOrderViewModel
        {
            CustomerId = preserve?.CustomerId ?? Guid.Empty,
            OfferId = preserve?.OfferId ?? Guid.Empty,
            Customers = customers
                .Where(c => c.Status == Crm.Domain.CustomerStatus.Active)
                .Select(c => new CustomerOption(c.Id, c.Name))
                .ToList(),
            Offers = offers
                .Where(o => o.IsActive)
                .Select(o => new OfferOption(o.Id, o.Name))
                .ToList()
        };
    }
}
