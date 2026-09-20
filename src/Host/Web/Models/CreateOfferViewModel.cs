using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public sealed class CreateOfferViewModel
{
    [Required]
    [StringLength(200)]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1, 100_000)]
    [Display(Name = "Download (Mbps)")]
    public int DownloadMbps { get; set; }

    [Required]
    [Range(1, 100_000)]
    [Display(Name = "Upload (Mbps)")]
    public int UploadMbps { get; set; }

    [Required]
    [Range(0, 1_000_000)]
    [Display(Name = "Monthly fee")]
    [DataType(DataType.Currency)]
    public decimal MonthlyFee { get; set; }
}
