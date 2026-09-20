using SharedKernel;

namespace Catalog.Domain;

public sealed class ProductOffer : AggregateRoot
{
    private ProductOffer()
    {
    }

    public string Name { get; private set; } = string.Empty;

    public int DownloadMbps { get; private set; }

    public int UploadMbps { get; private set; }

    public decimal MonthlyFee { get; private set; }

    public bool IsActive { get; private set; }

    public static Result<ProductOffer> Create(
        string name,
        int downloadMbps,
        int uploadMbps,
        decimal monthlyFee)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<ProductOffer>("Offer name is required.");

        if (downloadMbps <= 0)
            return Result.Failure<ProductOffer>("Download speed must be greater than zero.");

        if (uploadMbps <= 0)
            return Result.Failure<ProductOffer>("Upload speed must be greater than zero.");

        if (monthlyFee < 0)
            return Result.Failure<ProductOffer>("Monthly fee cannot be negative.");

        var offer = new ProductOffer
        {
            Name = name.Trim(),
            DownloadMbps = downloadMbps,
            UploadMbps = uploadMbps,
            MonthlyFee = monthlyFee,
            IsActive = true
        };

        return Result.Success(offer);
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure("Offer is already inactive.");

        IsActive = false;
        return Result.Success();
    }
}
