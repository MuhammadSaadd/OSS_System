using MediatR;

namespace Ordering.Application.Contracts;

/// <summary>
/// Integration event raised when a Draft order is submitted.
/// Provisioning reacts to start order-to-activate fulfillment (D5).
/// </summary>
public sealed record OrderSubmitted(
    Guid OrderId,
    Guid CustomerId,
    string CustomerName,
    Guid OfferId,
    string OfferName,
    decimal MonthlyFee) : INotification;
