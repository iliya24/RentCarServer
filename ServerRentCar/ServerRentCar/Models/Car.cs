using System;
using System.Collections.Generic;

#nullable disable

namespace ServerRentCar.Models
{
    public partial class Car
    {
        public Car()
        {
            CarRentRecords = new HashSet<CarRentRecord>();
        }

        public string LicensePlate { get; set; }
        public int CarsTypesId { get; set; }
        public string Kilometer { get; set; }
        public bool IsValidForRent { get; set; }
        public bool IsFreeForRent { get; set; }
        public int BranchId { get; set; }
        public byte[] CarImage { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CarsType CarsTypes { get; set; }
        public virtual ICollection<CarRentRecord> CarRentRecords { get; set; }
    }
}
