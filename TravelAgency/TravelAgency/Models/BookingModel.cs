using System.ComponentModel.DataAnnotations;
using BookingStatus = TravelAgencyWeb.Utils.Enumerations.BookingStatus;

namespace TravelAgency.Models;

public class BookingModel
{
    public int BookingId { get; set; }
    public string? UserId { get; set; }
    public int FlightId { get; set; }
    public string? UserFirstName { get; set; }
    public string? UserLastName { get; set; }
    public string? UserPhoneNumber { get; set; }
    public string? UserEmail { get; set; }
    public string? DepartureCity { get; set; }
    public string? DestinationCity { get; set; }
    [DataType(DataType.Date)]
    public DateTime DepartureTime { get; set; }
    public decimal Price { get; set; }
    [DataType(DataType.Date)]
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; }
}