using System.ComponentModel.DataAnnotations;
using TravelAgency.Models.Authentication;

namespace TravelAgencyTests
{
    [TestClass]
    public class RegisterViewModelTests
    {
        private RegisterViewModel CreateValidRegisterViewModel()
        {
            return new RegisterViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Password = "StrongPassword123!",
                ConfirmPassword = "StrongPassword123!"
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
        public void ValidRegisterViewModel_ShouldPassValidation()
        {
            var model = CreateValidRegisterViewModel();

            var results = ValidateModel(model);

            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void RegisterViewModel_MissingFirstName_ShouldFailValidation()
        {
            var model = CreateValidRegisterViewModel();
            model.FirstName = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The First Name field is required.")));
        }

        [TestMethod]
        public void RegisterViewModel_MissingLastName_ShouldFailValidation()
        {
            var model = CreateValidRegisterViewModel();
            model.LastName = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Last Name field is required.")));
        }

        [TestMethod]
        public void RegisterViewModel_InvalidEmailFormat_ShouldFailValidation()
        {
            var model = CreateValidRegisterViewModel();
            model.Email = "invalidemail";

            var results = ValidateModel(model);

            Assert.IsFalse(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Email field is not a valid email address.")));
        }

        [TestMethod]
        public void RegisterViewModel_MissingPhoneNumber_ShouldFailValidation()
        {
            var model = CreateValidRegisterViewModel();
            model.PhoneNumber = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Phone field is required.")));
        }

        [TestMethod]
        public void RegisterViewModel_InvalidPhoneNumberFormat_ShouldFailValidation()
        {
            var model = CreateValidRegisterViewModel();
            model.PhoneNumber = "invalidphone";

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Phone field is not a valid phone number.")));
        }

        [TestMethod]
        public void RegisterViewModel_MissingPassword_ShouldFailValidation()
        {
            var model = CreateValidRegisterViewModel();
            model.Password = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Password field is required.")));
        }

        [TestMethod]
        public void RegisterViewModel_MissingConfirmPassword_ShouldFailValidation()
        {
            var model = CreateValidRegisterViewModel();
            model.ConfirmPassword = null;

            var results = ValidateModel(model);

            Assert.IsFalse(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Confirm password field is required.")));
        }

        [TestMethod]
        public void RegisterViewModel_PasswordsDoNotMatch_ShouldFailValidation()
        {
            var model = CreateValidRegisterViewModel();
            model.ConfirmPassword = "MismatchedPassword123!";

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The password and confirmation password do not match.")));
        }
    }
}
