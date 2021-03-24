using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.Common.Atributes
{
    public class GenderValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object val, ValidationContext validationContext)
        {
            var gender = val.ToString().ToLower();
            if (gender=="male"|| gender=="female")
            {
                return ValidationResult.Success;
            }
            else
            {
                
                return new ValidationResult("Please choose a gender .(male or female");
            }
        }
    }
}
