using System;
using System.Collections.Generic;

#nullable disable

namespace ServerRentCar.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Cars = new HashSet<Car>();
        }

        public int BranchId { get; set; }
        public string BranchAdress { get; set; }
        public string BranchName { get; set; }
        public string Cordinate { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
