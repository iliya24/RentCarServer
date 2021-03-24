using ServerRentCar.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.DTO
{
    public class NewCarRentRecordDTO
    {
        
        [Required]
        public string StartRentDate { get; set; }
        [Required]
        public string EndRentDate { get; set; }        
       // public string ActualRentEndDate { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 7, ErrorMessage = "The LicensePlate should be 6 Length")]
        public string LicensePlate { get; set; }


    }
}
