using System.ComponentModel.DataAnnotations;
using TravelAgency.Models.UserRelated;
using TravelAgency.Utils.Enumerations;

namespace TravelAgencyTests
{
    [TestClass]
    public class AccountModelTests
    {
        private AccountModel CreateValidAccountModel()
        {
            return new AccountModel
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                FirstName = "John",
                LastName = "Doe",
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
        public void ValidAccountModel_ShouldPassValidation()
        {
            var model = CreateValidAccountModel();

            var results = ValidateModel(model);

            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void AccountModel_MissingFirstName_ShouldFailValidation()
        {
            var model = CreateValidAccountModel();
            model.FirstName = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("First name is required")));
        }

        [TestMethod]
        public void AccountModel_MissingLastName_ShouldFailValidation()
        {
            var model = CreateValidAccountModel();
            model.LastName = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("Last name is required")));
        }

        [TestMethod]
        public void AccountModel_LongFirstName_ShouldFailValidation()
        {
            var model = CreateValidAccountModel();
            model.FirstName = new string('A', 101);

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("First name cannot be longer than 100 characters")));
        }

        [TestMethod]
        public void AccountModel_LongLastName_ShouldFailValidation()
        {
            var model = CreateValidAccountModel();
            model.LastName = new string('A', 101);

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("Last name cannot be longer than 100 characters")));
        }
    }
}
