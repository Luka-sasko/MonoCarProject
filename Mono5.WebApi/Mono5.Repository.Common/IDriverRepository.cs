using Mono5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono5.Repository.Common
{
    public interface IDriverRepository
    {
        IEnumerable<Driver> GetAllDrivers();
        Driver FindDriverById(int id);
        void DeleteDriver(int id);
        void UpdateDriver(int id, DriverUpdate updatedDriver);
        void AddDriver(Driver driver);
    }
}
