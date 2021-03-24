using ServerRentCar.Common.Atributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.Auth
{
    public class RegisterModel
    {
       
        [Required]
        [StringLength(9,MinimumLength =9,ErrorMessage = "The id should be 9 lLength")]
        public string TeudZeut { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [AgeValidation(18)]
        public string BirthDate { get; set; }
        [Required]
        [GenderValidation]
        public string Gender { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
       
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "The Password should be 6 Length")]
        public string Password { get; set; }

    }
}
