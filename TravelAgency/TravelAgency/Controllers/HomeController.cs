using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyWeb.Models;
using TravelAgencyWeb.Models.Search;

namespace TravelAgencyWeb.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var searchModel = new SearchViewModel
        {
            DepartureDate = DateTime.Today,
            Quantity = 1    
        };

        return View(searchModel);
    }


    [HttpPost]
    public IActionResult Search(SearchViewModel searchViewModel)
    {
        return RedirectToAction("SearchResult", "Search", searchViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}