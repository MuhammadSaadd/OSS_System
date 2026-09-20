using MediatR;

namespace Catalog.Application.Offers.GetProductOffer;

public sealed record GetProductOfferQuery(Guid Id) : IRequest<ProductOfferDto?>;
