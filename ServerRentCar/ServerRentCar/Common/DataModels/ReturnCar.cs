﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.Common
{
    public class ReturnCar
    {
        [Required]
        [Range(0,1000000)]
        public string Kilometer { get; set; }
        [Required]
        [StringLength(9, MinimumLength = 7, ErrorMessage = "The LicensePlate should be 6 Length")]
        public string LicensePlate { get; set; }
        
    }
}
