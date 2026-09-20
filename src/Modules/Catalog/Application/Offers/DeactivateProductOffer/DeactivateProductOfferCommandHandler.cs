using Catalog.Application.Abstractions;
using MediatR;
using SharedKernel;

namespace Catalog.Application.Offers.DeactivateProductOffer;

public sealed class DeactivateProductOfferCommandHandler(IProductOfferRepository repository)
    : IRequestHandler<DeactivateProductOfferCommand, Result>
{
    public async Task<Result> Handle(
        DeactivateProductOfferCommand request,
        CancellationToken cancellationToken)
    {
        var offer = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (offer is null)
            return Result.Failure("Offer was not found.");

        var result = offer.Deactivate();
        if (result.IsFailure)
            return result;

        await repository.SaveChangesAsync(cancellationToken);
        return result;
    }
}
