using ServerRentCar.Common.Atributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServerRentCar.DTO
{
    public class CarsAsTableDTO
    {
        [Required]
        [StringLength(9, MinimumLength = 7, ErrorMessage = "The LicensePlate should be 6 Length")]

        public string LicensePlate { get; set; } = "";
        //[Required]
        //[Range(0, 1000000)]
        //public int CarsTypesId { get; set; }
        [Required]
        [Range(0, 1000000)]
        public string Kilometer { get; set; } = "0";
        [Required]
        public bool IsValidForRent { get; set; }
        [Required]
        public bool IsFreeForRent { get; set; }

        [Required]       
        public string BranchName { get; set; }
        [Required]
        public string CarImage { get; set; } = "";
        [Required]
        public string Manufacture { get; set; } = "";
        [Required]
        public string Model { get; set; } = "";
        [Required]
        public decimal PricePerDay { get; set; }
        [Required]
        public decimal DelayCostPerDay { get; set; }
        [Required]
        public string YearRelease { get; set; } 
        [Required]
        [GearValidation]
        public string Gear { get; set; } = "";


        public int branchId;
    }
}
