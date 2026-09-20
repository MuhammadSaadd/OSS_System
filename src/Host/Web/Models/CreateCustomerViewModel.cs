using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public sealed class CreateCustomerViewModel
{
    [Required]
    [StringLength(200)]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(320)]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    [Display(Name = "Address")]
    public string Address { get; set; } = string.Empty;
}
