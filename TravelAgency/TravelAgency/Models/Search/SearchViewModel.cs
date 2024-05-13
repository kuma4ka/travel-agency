namespace TravelAgencyWeb.Models.Search;

public class SearchViewModel
{
    public string? DepartureCity { get; set; }
    public string? ArrivalCity { get; set; }
    public DateTime DepartureDate { get; set; }
    public int Quantity { get; set; }
}