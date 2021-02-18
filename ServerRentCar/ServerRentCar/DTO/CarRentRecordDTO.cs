using ServerRentCar.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.DTO
{
    public class CarRentRecordDTO
    {
        public int RentRecordId { get; set; }
        public DateTime StartRentDate { get; set; }
        public DateTime EndRentDate { get; set; }
        public DateTime ActualRentEndDate { get; set; }
        public int UserId { get; set; }
        public string LicensePlate { get; set; }


    }
}
