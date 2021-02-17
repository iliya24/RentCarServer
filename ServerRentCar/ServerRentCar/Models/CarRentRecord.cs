using System;
using System.Collections.Generic;

#nullable disable

namespace ServerRentCar.Models
{
    public partial class CarRentRecord
    {
        public int RentRecordId { get; set; }
        public DateTime StartRentDate { get; set; }
        public DateTime EndRentDate { get; set; }
        public DateTime ActualRentEndDate { get; set; }
        public int UserId { get; set; }
        public string LicensePlate { get; set; }

        public virtual Car LicensePlateNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
