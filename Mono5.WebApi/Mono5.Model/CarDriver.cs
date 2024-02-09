using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono5.Model
{
    public class CarDriver
    {
        public int CarId { get; set; }
        public int DriverId { get; set; }
        public Car Car { get; set; }
        public Driver Driver { get; set; }
    }
}
