using System.ComponentModel.DataAnnotations;
using UserRole = TravelAgencyWeb.Utils.Enumerations.UserRole;

namespace TravelAgency.Models.UserRelated;

public class AddUserModel
{
    [Required]
    [Display(Name = "FirstName")]
    public string? FirstName { get; set; }

    [Required]
    [Display(Name = "LastName")]
    public string? LastName { get; set; }
    
    [Required]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required]
    [Display(Name = "Phone")]
    public string? Phone { get; set; }
    
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    public UserRole Role { get; set; } = 0;
}