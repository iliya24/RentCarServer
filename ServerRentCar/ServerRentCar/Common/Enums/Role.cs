using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ServerRentCar.Common.Enums
{
    public enum Role:byte
    {       
        Customer=0,
        Admin=1,
        Worker=2,
    }
}
