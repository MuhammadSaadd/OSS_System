using Ordering.Application.Abstractions;
using Ordering.Domain;
using MediatR;
using SharedKernel;

namespace Ordering.Application.Orders.CreateCustomerOrder;

public sealed class CreateCustomerOrderHandler(
    ICustomerOrderRepository repository,
    ICustomerDirectory customerDirectory,
    IOfferCatalog offerCatalog) : IRequestHandler<CreateCustomerOrderCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateCustomerOrderCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await customerDirectory.FindByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
            return Result.Failure<Guid>("Customer not found.");

        if (!customer.IsActive)
            return Result.Failure<Guid>($"Customer '{customer.Name}' is inactive.");

        var offer = await offerCatalog.FindByIdAsync(request.OfferId, cancellationToken);

        if (offer is null)
            return Result.Failure<Guid>("Offer not found.");

        if (!offer.IsActive)
            return Result.Failure<Guid>($"Offer '{offer.Name}' is inactive.");

        var createResult = CustomerOrder.Create(
            customer.Id,
            customer.Name,
            offer.Id,
            offer.Name,
            offer.MonthlyFee);

        if (createResult.IsFailure)
            return Result.Failure<Guid>(createResult.Error!);

        await repository.AddAsync(createResult.Value, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return Result.Success(createResult.Value.Id);
    }
}
