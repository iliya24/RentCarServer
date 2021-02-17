using System;
using System.Collections.Generic;

#nullable disable

namespace ServerRentCar.Models
{
    public partial class User
    {
        public User()
        {
            CarRentRecords = new HashSet<CarRentRecord>();
        }

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

        public virtual ICollection<CarRentRecord> CarRentRecords { get; set; }
    }
}
