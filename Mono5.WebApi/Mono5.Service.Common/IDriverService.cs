using Mono5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono5.Service.Common
{
    public interface IDriverService
    {
        Driver GetDriverById(int id);
        IEnumerable<Driver> GetAllDrivers();
        void AddDriver(Driver driver);
        void UpdateDriver(int id, DriverUpdate editedDriver);
        void DeleteDriver(int id);
    }
}
