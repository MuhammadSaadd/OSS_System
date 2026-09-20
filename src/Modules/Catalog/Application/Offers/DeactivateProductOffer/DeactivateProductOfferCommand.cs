using MediatR;
using SharedKernel;

namespace Catalog.Application.Offers.DeactivateProductOffer;

public sealed record DeactivateProductOfferCommand(Guid Id) : IRequest<Result>;
