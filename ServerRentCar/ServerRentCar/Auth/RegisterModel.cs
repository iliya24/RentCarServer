using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.Auth
{
    public class RegisterModel
    {
        public string TeudZeut { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
