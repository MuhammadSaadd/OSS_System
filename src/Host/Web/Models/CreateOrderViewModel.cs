using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public sealed class CreateOrderViewModel
{
    [Required]
    [Display(Name = "Customer")]
    public Guid CustomerId { get; set; }

    [Required]
    [Display(Name = "Offer")]
    public Guid OfferId { get; set; }

    public IReadOnlyList<CustomerOption> Customers { get; set; } = [];

    public IReadOnlyList<OfferOption> Offers { get; set; } = [];
}

public sealed record CustomerOption(Guid Id, string Name);

public sealed record OfferOption(Guid Id, string Name);
