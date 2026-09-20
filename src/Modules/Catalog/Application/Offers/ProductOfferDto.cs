namespace Catalog.Application.Offers;

public sealed record ProductOfferDto(
    Guid Id,
    string Name,
    int DownloadMbps,
    int UploadMbps,
    decimal MonthlyFee,
    bool IsActive);
