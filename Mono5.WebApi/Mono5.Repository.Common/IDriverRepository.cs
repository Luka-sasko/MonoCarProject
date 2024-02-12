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
        Task<IEnumerable<Driver>> GetAllDrivers();
        Task<Driver> FindDriverById(int id);
        Task DeleteDriver(int id);
        Task UpdateDriver(int id, DriverUpdate updatedDriver);
        Task AddDriver(Driver driver);
    }
}
