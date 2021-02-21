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
        public string  CarImage { get; set; }

        public string Manufacture { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal DelayCostPerDay { get; set; }
        public DateTime YearRelease { get; set; }
        public string Gear { get; set; }
    }
}
