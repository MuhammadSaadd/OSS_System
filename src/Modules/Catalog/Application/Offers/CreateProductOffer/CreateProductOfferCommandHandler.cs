using Catalog.Application.Abstractions;
using Catalog.Domain;
using MediatR;
using SharedKernel;

namespace Catalog.Application.Offers.CreateProductOffer;

public sealed class CreateProductOfferCommandHandler(IProductOfferRepository repository)
    : IRequestHandler<CreateProductOfferCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateProductOfferCommand request,
        CancellationToken cancellationToken)
    {
        if (await repository.ExistsByNameAsync(request.Name, cancellationToken))
            return Result.Failure<Guid>($"An offer named '{request.Name.Trim()}' already exists.");

        var createResult = ProductOffer.Create(
            request.Name,
            request.DownloadMbps,
            request.UploadMbps,
            request.MonthlyFee);

        if (createResult.IsFailure)
            return Result.Failure<Guid>(createResult.Error!);

        await repository.AddAsync(createResult.Value, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return Result.Success(createResult.Value.Id);
    }
}
