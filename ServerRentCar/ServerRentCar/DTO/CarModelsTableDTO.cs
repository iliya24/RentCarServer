using ServerRentCar.Common.Atributes;
using ServerRentCar.Common.Converters;
using ServerRentCar.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServerRentCar.DTO
{
    public class CarModelsTableDTO
    {
         
        public string Manufacture { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal DelayCostPerDay { get; set; }
        public string YearRelease { get; set; }

        public string Gear { get; set; }


    }
}
