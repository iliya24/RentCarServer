using System;
using System.ComponentModel.DataAnnotations;

namespace ServerRentCar.Auth
{
    internal class AgeValidationAttribute : ValidationAttribute
    {
        private int _mimimalAge;

        public AgeValidationAttribute(int mimimalAge)
        {
            mimimalAge= _mimimalAge;
        }

        protected override ValidationResult IsValid(object val, ValidationContext validationContext)
        {
            DateTime age ;

            if(DateTime.TryParse(val.ToString(),out age))
            {
                if (CalculateAge(age) >= 18)
                    return ValidationResult.Success;
                else return new ValidationResult("Age is need to be above 18");

            }
                return new ValidationResult("Set correct value showld be a Date Time format");
            
        }

        private  int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}