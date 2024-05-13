using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Models.Admin;
using TravelAgency.Models.UserRelated;
using TravelAgencyWeb.Data;
using TravelAgencyWeb.Models;
using TravelAgencyWeb.Utils;
using TravelAgencyWeb.Utils.Interfaces;
using UserRole = TravelAgencyWeb.Utils.Enumerations.UserRole;

namespace TravelAgencyWeb.Controllers
{
        public class AdminController(UserManager<AccountModel> userManager, ApplicationDbContext context)
            : Controller, IAdmin
        {
            [HttpGet]
        public async Task<IActionResult> AdminPanel()
        {
            var user = await userManager.GetUserAsync(User);

            if (user is not { Role: UserRole.Admin })
            {
                return RedirectToAction("Index", "Home");
            }
            
            var allUsers = userManager.Users.ToList();
            
            var usersExceptCurrent = allUsers
                .Where(u => u.Id != user.Id).ToList();
            
            var articles = context.Flights.ToList();

            AdminPanelViewModel model = new() {
                AccModels = usersExceptCurrent,
                FlightModels = articles
            };
            return View(model);
        }
        
        [Authorize]
        public async Task<IActionResult> AddUser()
        {
            var newUser = new AddUserModel();
            return View(newUser);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUser(AddUserModel newUser)
        {
            if (!ModelState.IsValid) return View(newUser);
            
            var existingUser = await FindBy.FindByEmailAsync(newUser.Email, userManager);
            
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "This email address is already in use!");
                return View(newUser);
            }
            
            var user = new AccountModel
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                UserName = newUser.Email,
                Email = newUser.Email,
                PhoneNumber = newUser.Phone
            };        
            
            var result = await userManager.CreateAsync(user, newUser.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("AdminPanel");
            }
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(newUser);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string userId, string firstName, string lastName, string email, string phone, UserRole role)
        {
            var user = await userManager.FindByIdAsync(userId);
    
            if (user == null)
            {
                return RedirectToAction("AdminPanel");
            }
    
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.PhoneNumber = phone;
            user.Role = role;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(); 
            }

            return RedirectToAction("AdminPanel");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            
            if (user is null) return RedirectToAction("AdminPanel");
            
            await userManager.DeleteAsync(user);
            await context.SaveChangesAsync();

            return RedirectToAction("AdminPanel");
        }
        
        [Authorize]
        public async Task<IActionResult> AddFlight()
        {
            var model = new FlightModel
            {
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now, 
            };
            
            return View(model);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddFlight(FlightModel newFlight)
        {
            if (!ModelState.IsValid) return View(newFlight);
            
            var existingArticle = await context.Flights.FirstOrDefaultAsync(f => f.FlightId == newFlight.FlightId);
            
            if (existingArticle != null)
            {
                ModelState.AddModelError(string.Empty, "Flight with such id is already exist!");
                return View(newFlight);
            }

            var flight = new FlightModel
            {
                DepartureCity = newFlight.DepartureCity,
                DestinationCity = newFlight.DestinationCity,
                DepartureTime = newFlight.DepartureTime,
                ArrivalTime = newFlight.ArrivalTime,
                Price = newFlight.Price,
                IsAvailable = newFlight.IsAvailable,
                ImageUrl = newFlight.ImageUrl,
                Description = newFlight.Description
            };
            
            flight.DestinationCity = Request.Form["DestinationCity"];
            
            await context.Flights.AddAsync(flight);
            await context.SaveChangesAsync();

            return RedirectToAction("AdminPanel");

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateFlight(int flightId, string departureCity, string destinationCity, 
            DateTime departureTime, DateTime arrivalTime, decimal price, bool isAvailable, string? imageUrl, string? description)
        {
            var flight = await context.Flights.FindAsync(flightId);

            if (flight == null) return RedirectToAction("AdminPanel");

            flight.DepartureCity = departureCity;
            flight.DestinationCity = destinationCity;
            flight.DepartureTime = departureTime;
            flight.ArrivalTime = arrivalTime;
            flight.Price = price;
            flight.IsAvailable = isAvailable;
            flight.ImageUrl = imageUrl;
            flight.Description = description;

            await context.SaveChangesAsync();

            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteFlight(int flightId)
        {
            var flight = await context.Flights.FindAsync(flightId);

            if (flight == null) return RedirectToAction("AdminPanel");

            context.Flights.Remove(flight);
            await context.SaveChangesAsync();

            return RedirectToAction("AdminPanel");
        }
    }
}