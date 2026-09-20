using Ordering.Domain;

namespace Ordering.Application.Orders;

public sealed record CustomerOrderDto(
    Guid Id,
    Guid CustomerId,
    string CustomerName,
    Guid OfferId,
    string OfferName,
    decimal MonthlyFee,
    OrderStatus Status);
