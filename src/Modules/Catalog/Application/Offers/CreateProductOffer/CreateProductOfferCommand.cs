using MediatR;
using SharedKernel;

namespace Catalog.Application.Offers.CreateProductOffer;

public sealed record CreateProductOfferCommand(
    string Name,
    int DownloadMbps,
    int UploadMbps,
    decimal MonthlyFee) : IRequest<Result<Guid>>;
