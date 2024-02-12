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
        Task<Driver> GetDriverById(int id);
        Task<IEnumerable<Driver>> GetAllDrivers();
        Task AddDriver(Driver driver);
        Task UpdateDriver(int id, DriverUpdate editedDriver);
        Task DeleteDriver(int id);
    }
}
