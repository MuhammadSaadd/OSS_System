namespace Ordering.Application.Abstractions;

public sealed record CustomerSnapshot(Guid Id, string Name, bool IsActive);

public sealed record OfferSnapshot(Guid Id, string Name, decimal MonthlyFee, bool IsActive);
