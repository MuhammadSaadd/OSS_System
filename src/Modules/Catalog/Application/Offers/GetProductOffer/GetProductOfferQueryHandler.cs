using Catalog.Application.Abstractions;
using MediatR;

namespace Catalog.Application.Offers.GetProductOffer;

public sealed class GetProductOfferQueryHandler(IProductOfferRepository repository)
    : IRequestHandler<GetProductOfferQuery, ProductOfferDto?>
{
    public async Task<ProductOfferDto?> Handle(
        GetProductOfferQuery request,
        CancellationToken cancellationToken)
    {
        var offer = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (offer is null)
            return null;

        return new ProductOfferDto(
            offer.Id,
            offer.Name,
            offer.DownloadMbps,
            offer.UploadMbps,
            offer.MonthlyFee,
            offer.IsActive);
    }
}
