using Catalog.Application.Offers.CreateProductOffer;
using Catalog.Application.Offers.DeactivateProductOffer;
using Catalog.Application.Offers.ListProductOffers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class OffersController(IMediator mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var offers = await mediator.Send(new ListProductOffersQuery(), cancellationToken);
        return View(offers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateOfferViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOfferViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await mediator.Send(
            new CreateProductOfferCommand(
                model.Name,
                model.DownloadMbps,
                model.UploadMbps,
                model.MonthlyFee),
            cancellationToken);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return View(model);
        }

        TempData["Success"] = "Offer created.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeactivateProductOfferCommand(id), cancellationToken);

        TempData[result.IsSuccess ? "Success" : "Error"] =
            result.IsSuccess ? "Offer deactivated." : result.Error;

        return RedirectToAction(nameof(Index));
    }
}
