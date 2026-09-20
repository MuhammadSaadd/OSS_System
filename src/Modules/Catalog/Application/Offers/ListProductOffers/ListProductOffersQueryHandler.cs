using Catalog.Application.Abstractions;
using MediatR;

namespace Catalog.Application.Offers.ListProductOffers;

public sealed class ListProductOffersQueryHandler(IProductOfferRepository repository)
    : IRequestHandler<ListProductOffersQuery, IReadOnlyList<ProductOfferDto>>
{
    public async Task<IReadOnlyList<ProductOfferDto>> Handle(
        ListProductOffersQuery request,
        CancellationToken cancellationToken)
    {
        var offers = await repository.ListAsync(cancellationToken);

        return offers
            .Select(o => new ProductOfferDto(
                o.Id,
                o.Name,
                o.DownloadMbps,
                o.UploadMbps,
                o.MonthlyFee,
                o.IsActive))
            .ToList();
    }
}
