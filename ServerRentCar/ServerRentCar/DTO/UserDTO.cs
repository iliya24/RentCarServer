using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string TeudZeut { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool Sex { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] UserPicture { get; set; }
        public byte Role { get; set; }

    }
}
