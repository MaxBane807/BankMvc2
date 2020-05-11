using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ValidationAttributes
{
    public class NotLessThanZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (int.Parse((value.ToString())) <= 0)
            {
                return new ValidationResult("Value must be greater than zero");
            }
            
            return ValidationResult.Success;
        }
    }
}
