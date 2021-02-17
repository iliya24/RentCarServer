using System;
using System.Collections.Generic;

#nullable disable

namespace ServerRentCar.Models
{
    public partial class CarsType
    {
        public CarsType()
        {
            Cars = new HashSet<Car>();
        }

        public int CarsTypesId { get; set; }
        public string Manufacture { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal DelayCostPerDay { get; set; }
        public DateTime YearRelease { get; set; }
        public string Gear { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
