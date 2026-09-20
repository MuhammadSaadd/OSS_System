using MediatR;

namespace Catalog.Application.Offers.ListProductOffers;

public sealed record ListProductOffersQuery : IRequest<IReadOnlyList<ProductOfferDto>>;
