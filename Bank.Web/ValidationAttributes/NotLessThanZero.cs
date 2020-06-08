using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ValidationAttributes
{
    public class NotLessThanZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (decimal.Parse((value.ToString())) <= 0)
            {
                return new ValidationResult("Value must be greater than zero");
            }
            
            return ValidationResult.Success;
        }
    }
}
