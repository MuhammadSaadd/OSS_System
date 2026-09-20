using SharedKernel;

namespace Ordering.Domain;

public sealed class CustomerOrder : AggregateRoot
{
    private CustomerOrder()
    {
    }

    public Guid CustomerId { get; private set; }

    public string CustomerName { get; private set; } = string.Empty;

    public Guid OfferId { get; private set; }

    public string OfferName { get; private set; } = string.Empty;

    public decimal MonthlyFee { get; private set; }

    public OrderStatus Status { get; private set; }

    public static Result<CustomerOrder> Create(
        Guid customerId,
        string customerName,
        Guid offerId,
        string offerName,
        decimal monthlyFee)
    {
        if (customerId == Guid.Empty)
            return Result.Failure<CustomerOrder>("Customer is required.");

        if (string.IsNullOrWhiteSpace(customerName))
            return Result.Failure<CustomerOrder>("Customer name is required.");

        if (offerId == Guid.Empty)
            return Result.Failure<CustomerOrder>("Offer is required.");

        if (string.IsNullOrWhiteSpace(offerName))
            return Result.Failure<CustomerOrder>("Offer name is required.");

        if (monthlyFee < 0)
            return Result.Failure<CustomerOrder>("Monthly fee cannot be negative.");

        var order = new CustomerOrder
        {
            CustomerId = customerId,
            CustomerName = customerName.Trim(),
            OfferId = offerId,
            OfferName = offerName.Trim(),
            MonthlyFee = monthlyFee,
            Status = OrderStatus.Draft
        };

        return Result.Success(order);
    }

    public Result Submit()
    {
        if (Status != OrderStatus.Draft)
            return Result.Failure($"Order can only be submitted from Draft, current status is {Status}.");

        Status = OrderStatus.Submitted;
        return Result.Success();
    }
}
