using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.Common.Atributes
{
    public class GearValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object val, ValidationContext validationContext)
        {
            var gear = val.ToString().ToLower();
            if (gear == "automatic"|| gear == "manual"|| gear == "auto")
            {
                return ValidationResult.Success;
            }
            else
            {
                
                return new ValidationResult("Please choose a gender .(automatic or manual or auto");
            }
        }
    }
}
