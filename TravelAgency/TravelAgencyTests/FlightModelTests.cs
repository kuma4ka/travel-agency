using System.ComponentModel.DataAnnotations;
using TravelAgency.Models;

namespace TravelAgencyTests
{
    [TestClass]
    public class FlightModelTests
    {
        private FlightModel CreateValidFlightModel()
        {
            return new FlightModel
            {
                FlightId = 1,
                DepartureCity = "New York",
                DestinationCity = "Los Angeles",
                DepartureTime = DateTime.Now.AddHours(1),
                ArrivalTime = DateTime.Now.AddHours(5),
                Price = 200.50m,
                IsAvailable = true,
                Description = "A flight from New York to Los Angeles",
                ImageUrl = "http://example.com/image.jpg"
            };
        }

        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [TestMethod]
        public void ValidFlightModel_ShouldPassValidation()
        {
            var model = CreateValidFlightModel();

            var results = ValidateModel(model);

            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void FlightModel_MissingDepartureCity_ShouldFailValidation()
        {
            var model = CreateValidFlightModel();
            model.DepartureCity = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage.Contains("Departure City is required")));
        }

        [TestMethod]
        public void FlightModel_DepartureCityExceedsMaxLength_ShouldFailValidation()
        {
            var model = CreateValidFlightModel();
            model.DepartureCity = new string('a', 101);

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("Departure City cannot be longer than 100 characters.")));
        }

        [TestMethod]
        public void FlightModel_ArrivalTimeBeforeDepartureTime_ShouldFailValidation()
        {
            var model = CreateValidFlightModel();
            model.ArrivalTime = model.DepartureTime.AddMinutes(-30);

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("Arrival Time must be later than Departure Time")));
        }

        [TestMethod]
        public void FlightModel_NegativePrice_ShouldFailValidation()
        {
            var model = CreateValidFlightModel();
            model.Price = -1;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("Price must be a positive value")));
        }
    }
}
