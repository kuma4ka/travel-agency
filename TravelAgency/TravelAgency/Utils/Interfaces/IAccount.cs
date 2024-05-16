using Microsoft.AspNetCore.Mvc;

namespace TravelAgencyWeb.Utils.Interfaces;

public interface IAccount
{
    public IActionResult Register();
    public IActionResult Login(string returnUrl);
    public Task<IActionResult> Logout();
    public IActionResult ChangePassword();
    public Task<IActionResult> ViewProfile();
}