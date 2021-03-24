using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.Common.Atributes
{
    public class RoleValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object val, ValidationContext validationContext)
        {
            var role = val.ToString().ToLower();
            if (role == "admin"|| role == "worker" || role=="customer")
            {
                return ValidationResult.Success;
            }
            else
            {                
                return new ValidationResult("Please choose a role .(admin or worker or customer");
            }
        }
    }
}
