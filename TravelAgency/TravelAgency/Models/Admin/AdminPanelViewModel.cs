using TravelAgency.Models.UserRelated;
using TravelAgencyWeb.Models;

namespace TravelAgency.Models.Admin;

public class AdminPanelViewModel
{
    public List<AccountModel>? AccModels { get; set; }
    public List<FlightModel>? FlightModels { get; set; }
}