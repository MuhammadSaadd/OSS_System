using Ordering.Application.Abstractions;
using Ordering.Application.Contracts;
using MediatR;
using SharedKernel;

namespace Ordering.Application.Orders.SubmitCustomerOrder;

public sealed class SubmitCustomerOrderHandler(
    ICustomerOrderRepository repository,
    IPublisher publisher) : IRequestHandler<SubmitCustomerOrderCommand, Result>
{
    public async Task<Result> Handle(
        SubmitCustomerOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result.Failure("Order not found.");

        var submitResult = order.Submit();

        if (submitResult.IsFailure)
            return Result.Failure(submitResult.Error!);

        await repository.SaveChangesAsync(cancellationToken);

        // In-process sync dispatch: D5 provisioning reacts to this.
        await publisher.Publish(
            new OrderSubmitted(
                order.Id,
                order.CustomerId,
                order.CustomerName,
                order.OfferId,
                order.OfferName,
                order.MonthlyFee),
            cancellationToken);

        return Result.Success();
    }
}
