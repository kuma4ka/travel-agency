using System.ComponentModel.DataAnnotations;
using TravelAgency.Models;
using TravelAgency.Utils.Enumerations;

namespace TravelAgencyTests
{
    [TestClass]
    public class BookingModelTests
    {
        private BookingModel CreateValidBookingModel()
        {
            return new BookingModel
            {
                BookingId = 1,
                UserId = "user123",
                FlightId = 101,
                UserFirstName = "John",
                UserLastName = "Doe",
                UserPhoneNumber = "1234567890",
                UserEmail = "john.doe@example.com",
                DepartureCity = "New York",
                DestinationCity = "Los Angeles",
                DepartureTime = DateTime.Now.AddDays(1),
                Price = 200.50m,
                BookingDate = DateTime.Now,
                Status = BookingStatus.Confirmed
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
        public void ValidBookingModel_ShouldPassValidation()
        {
            var model = CreateValidBookingModel();

            var results = ValidateModel(model);

            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void BookingModel_MissingUserId_ShouldFailValidation()
        {
            var model = new BookingModel
            {
                BookingId = 1,
                UserId = null,
                FlightId = 101,
                UserFirstName = "John",
                UserLastName = "Doe",
                UserPhoneNumber = "1234567890",
                UserEmail = "john.doe@example.com",
                DepartureCity = "New York",
                DestinationCity = "Los Angeles",
                DepartureTime = DateTime.Now.AddDays(1),
                Price = 200.50m,
                BookingDate = DateTime.Now,
                Status = BookingStatus.Confirmed
            };

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("User ID is required")));
        }

        [TestMethod]
        public void BookingModel_UserIdExceedsMaxLength_ShouldFailValidation()
        {
            var model = new BookingModel
            {
                BookingId = 1,
                UserId = new string('a', 51),
                FlightId = 101,
                UserFirstName = "John",
                UserLastName = "Doe",
                UserPhoneNumber = "1234567890",
                UserEmail = "john.doe@example.com",
                DepartureCity = "New York",
                DestinationCity = "Los Angeles",
                DepartureTime = DateTime.Now.AddDays(1),
                Price = 200.50m,
                BookingDate = DateTime.Now,
                Status = BookingStatus.Confirmed
            };

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("User id cannot be longer than 50 characters.")));
        }

        [TestMethod]
        public void BookingModel_InvalidPhoneNumber_ShouldFailValidation()
        {
            var model = new BookingModel
            {
                BookingId = 1,
                UserId = "user123",
                FlightId = 101,
                UserFirstName = "John",
                UserLastName = "Doe",
                UserPhoneNumber = "invalid-phone",
                UserEmail = "john.doe@example.com",
                DepartureCity = "New York",
                DestinationCity = "Los Angeles",
                DepartureTime = DateTime.Now.AddDays(1),
                Price = 200.50m,
                BookingDate = DateTime.Now,
                Status = BookingStatus.Confirmed
            };

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("Invalid phone number format")));
        }

        [TestMethod]
        public void BookingModel_InvalidEmail_ShouldFailValidation()
        {
            var model = new BookingModel
            {
                BookingId = 1,
                UserId = "user123",
                FlightId = 101,
                UserFirstName = "John",
                UserLastName = "Doe",
                UserPhoneNumber = "1234567890",
                UserEmail = "invalid-email",
                DepartureCity = "New York",
                DestinationCity = "Los Angeles",
                DepartureTime = DateTime.Now.AddDays(1),
                Price = 200.50m,
                BookingDate = DateTime.Now,
                Status = BookingStatus.Confirmed
            };

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("Invalid email address format")));
        }

        [TestMethod]
        public void BookingModel_NegativePrice_ShouldFailValidation()
        {
            var model = new BookingModel
            {
                BookingId = 1,
                UserId = "user123",
                FlightId = 101,
                UserFirstName = "John",
                UserLastName = "Doe",
                UserPhoneNumber = "1234567890",
                UserEmail = "john.doe@example.com",
                DepartureCity = "New York",
                DestinationCity = "Los Angeles",
                DepartureTime = DateTime.Now.AddDays(1),
                Price = -1,
                BookingDate = DateTime.Now,
                Status = BookingStatus.Confirmed
            };

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("Price must be a positive value")));
        }
    }
}
