using ServerRentCar.Common.Atributes;
using ServerRentCar.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.DTO
{
    public class CarTableDTO
    {
        public string LicensePlate { get; set; }
        public string CarsTypesId { get; set; }
        public string Kilometer { get; set; }
        public string IsValidForRent { get; set; }
        public string IsFreeForRent { get; set; }
        public string BranchId { get; set; }
    }
}
