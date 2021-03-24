using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.Auth
{
    public class LogInModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "The Password should be 6 Length")]
        public string Password { get; set; }

    }
}
