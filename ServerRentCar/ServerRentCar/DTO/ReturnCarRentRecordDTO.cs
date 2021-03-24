using ServerRentCar.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.DTO
{
    public class ReturnCarRentRecordDTO
    {
        // public int RentRecordId { get; set; }
       
        [Required]
        public DateTime ActualRentEndDate { get; set; }


        [Required]
        public string TotalCost { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 7, ErrorMessage = "The LicensePlate should be 6 Length")]
        public string LicensePlate { get; set; }


    }
}
