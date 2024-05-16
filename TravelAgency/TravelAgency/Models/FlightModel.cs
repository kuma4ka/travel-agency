using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyWeb.Models
{
    public class FlightModel
    {
        [Required]
        public int FlightId { get; set; }
        
        [Required(ErrorMessage = "Departure City is required")]
        public string? DepartureCity { get; set; }
        
        [Required(ErrorMessage = "Destination City is required")]
        public string? DestinationCity { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DepartureTime { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime ArrivalTime { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public bool IsAvailable { get; set; }
        
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        
        [Required]
        public string? ImageUrl { get; set; }
    }
}