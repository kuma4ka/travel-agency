using Microsoft.AspNetCore.Identity;
using UserRole = TravelAgencyWeb.Utils.Enumerations.UserRole;

namespace TravelAgency.Models.UserRelated;

public class AccountModel : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public UserRole Role { get; set; } = 0;
}