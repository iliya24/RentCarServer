using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.DTO
{
    public class CarsDTO
    {
        public string LicensePlate { get; set; }
        public int CarsTypesId { get; set; }
        public string Kilometer { get; set; }
        public bool IsValidForRent { get; set; }
        public bool IsFreeForRent { get; set; }
        public int BranchId { get; set; }
        public byte[] CarImage { get; set; }
    }
}
