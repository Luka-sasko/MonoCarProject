using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Mono5.Model;
namespace Mono5.Repository.Common
{
    public interface ICarDriverRepository
    {

        Task<Driver> FindDriverById(int id);
        Task<Car> FindCarById(int id);
        Task DeleteCarDriver(int driverId, int carId);
        Task<int> UpdateCarDriver(int carId, int driverId, int newDriverId);
        Task AddCarDriver(int carId, int driverId);
        Task<IEnumerable<Driver>> GetAllCarsDrivers(int carId);
    }
}
