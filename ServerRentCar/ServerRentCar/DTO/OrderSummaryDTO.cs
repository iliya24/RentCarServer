using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerRentCar.DTO
{
    public class OrderSummaryDTO
    {
        public string OrderId { get; set; }
        public string  Manufacture { get; set; }
        public string Model { get; set; }
        public string PricePerDay { get; set; }
        public string DelayCostPerDay { get; set; }
        public string YearRelease { get; set; }
        public string Gear { get; set; }
        public string TotalCost { get; set; }
        public string StartRentDate { get; set; }
        public string EndRentDate { get; set; }      
        public string ActualRentEndDate { get; set; }

    }
}
