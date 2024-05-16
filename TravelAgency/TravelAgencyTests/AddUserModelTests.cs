using System.ComponentModel.DataAnnotations;
using TravelAgency.Models.UserRelated;
using TravelAgency.Utils.Enumerations;

namespace TravelAgencyTests
{
    [TestClass]
    public class AddUserModelTests
    {
        private AddUserModel CreateValidAddUserModel()
        {
            return new AddUserModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Password = "StrongPassword123!",
                Role = UserRole.User
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
        public void ValidAddUserModel_ShouldPassValidation()
        {
            var model = CreateValidAddUserModel();

            var results = ValidateModel(model);

            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void AddUserModel_MissingFirstName_ShouldFailValidation()
        {
            var model = CreateValidAddUserModel();
            model.FirstName = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The FirstName field is required.")));
        }

        [TestMethod]
        public void AddUserModel_MissingLastName_ShouldFailValidation()
        {
            var model = CreateValidAddUserModel();
            model.LastName = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The LastName field is required.")));
        }

        [TestMethod]
        public void AddUserModel_MissingEmail_ShouldFailValidation()
        {
            var model = CreateValidAddUserModel();
            model.Email = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Email field is required.")));
        }

        [TestMethod]
        public void AddUserModel_MissingPhone_ShouldFailValidation()
        {
            var model = CreateValidAddUserModel();
            model.Phone = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Phone field is required.")));
        }

        [TestMethod]
        public void AddUserModel_MissingPassword_ShouldFailValidation()
        {
            var model = CreateValidAddUserModel();
            model.Password = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Password field is required.")));
        }

        [TestMethod]
        public void AddUserModel_ShortPassword_ShouldFailValidation()
        {
            var model = CreateValidAddUserModel();
            model.Password = "short";

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Password must be at least 8 and at max 100 characters long.")));
        }
    }
}
