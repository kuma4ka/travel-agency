using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Utils.Validation;

public class DateGreaterThanAttribute(string comparisonProperty) : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var currentValue = (DateTime)value;

        var property = validationContext.ObjectType.GetProperty(comparisonProperty);
        if (property == null)
        {
            return new ValidationResult($"Unknown property: {comparisonProperty}");
        }

        var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

        if (currentValue <= comparisonValue)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}