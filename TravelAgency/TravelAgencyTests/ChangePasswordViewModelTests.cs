using System.ComponentModel.DataAnnotations;
using TravelAgency.Models.UserRelated;

namespace TravelAgencyTests
{
    [TestClass]
    public class ChangePasswordViewModelTests
    {
        private ChangePasswordViewModel CreateValidChangePasswordViewModel()
        {
            return new ChangePasswordViewModel
            {
                CurrentPassword = "CurrentPassword123!",
                NewPassword = "NewPassword123!",
                ConfirmPassword = "NewPassword123!",
                Changed = true
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
        public void ValidChangePasswordViewModel_ShouldPassValidation()
        {
            var model = CreateValidChangePasswordViewModel();

            var results = ValidateModel(model);

            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void ChangePasswordViewModel_MissingCurrentPassword_ShouldFailValidation()
        {
            var model = CreateValidChangePasswordViewModel();
            model.CurrentPassword = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Current password field is required.")));
        }

        [TestMethod]
        public void ChangePasswordViewModel_MissingNewPassword_ShouldFailValidation()
        {
            var model = CreateValidChangePasswordViewModel();
            model.NewPassword = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The New password field is required.")));
        }

        [TestMethod]
        public void ChangePasswordViewModel_ShortNewPassword_ShouldFailValidation()
        {
            var model = CreateValidChangePasswordViewModel();
            model.NewPassword = "short";

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("The New password must be at least 8 and at max 100 characters long.")));
        }

        [TestMethod]
        public void ChangePasswordViewModel_MissingConfirmPassword_ShouldFailValidation()
        {
            var model = CreateValidChangePasswordViewModel();
            model.ConfirmPassword = null;

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("Passwords do not match!")));
        }


        [TestMethod]
        public void ChangePasswordViewModel_PasswordsDoNotMatch_ShouldFailValidation()
        {
            var model = CreateValidChangePasswordViewModel();
            model.ConfirmPassword = "DifferentPassword123!";

            var results = ValidateModel(model);

            Assert.IsTrue(results.Any(v => v.ErrorMessage != null && v.ErrorMessage.Contains("Passwords do not match!")));
        }
    }
}
