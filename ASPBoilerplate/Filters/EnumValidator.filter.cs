using System;
using System.ComponentModel.DataAnnotations;

public class EnumValidatorFilter : ValidationAttribute
{
    private readonly Type _enumType;

    public EnumValidatorFilter(Type enumType)
    {
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("Type must be an enum.");
        }

        _enumType = enumType;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || !Enum.IsDefined(_enumType, value))
        {
            return new ValidationResult($"{value} is not a valid value for {_enumType.Name}");
        }

        return ValidationResult.Success;
    }
}
