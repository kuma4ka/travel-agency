using Microsoft.AspNetCore.Mvc;
using TravelAgencyWeb.Utils.Enumerations;

namespace TravelAgencyWeb.Utils.Interfaces;

public interface IAdmin
{
    public Task<IActionResult> AdminPanel();
    public Task<IActionResult> AddUser();
    public Task<IActionResult> UpdateUser(string userId, string firstName, string lastName, string email, string phone, UserRole role);
    public Task<IActionResult> DeleteUser(string userId);
    public Task<IActionResult> AddFlight();
    public Task<IActionResult> UpdateFlight(int flightId, string departureCity, string destinationCity, DateTime departureTime, DateTime arrivalTime, decimal price, bool isAvailable, string? imageUrl, string? description);
    public Task<IActionResult> DeleteFlight(int flightId);
}