using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyWeb.Data;
using TravelAgencyWeb.Models.Search;

namespace TravelAgencyWeb.Controllers
{
    public class SearchController(ApplicationDbContext context) : Controller
    {
        public async Task<IActionResult> SearchResult(SearchViewModel searchViewModel)
        {
            var matchingFlights = await context.Flights
                .Where(f =>
                    f.DepartureCity == searchViewModel.DepartureCity &&
                    f.DestinationCity == searchViewModel.ArrivalCity &&
                    f.DepartureTime == searchViewModel.DepartureDate)
                .ToListAsync();

            var searchResultModel = new SearchResultModel
            {
                Flights = matchingFlights
            };

            if (!ModelState.IsValid)
            {
                return View(searchResultModel);
            }

            return View(searchResultModel);
        }
        
    }
}
