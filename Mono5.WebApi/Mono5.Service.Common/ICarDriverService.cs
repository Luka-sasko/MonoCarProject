using Mono5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono5.Service.Common
{
    public interface ICarDriverService
    {
        Driver FindDriverById(int id);
        Car FindCarById(int id);
        void DeleteCarDriver(int driverId, int carId);
        void UpdateCarDriver(int carId, int driverId, int newDriverId);
        void AddCarDriver(int carId, int driverId);
        IEnumerable<Driver> GetAllCarsDrivers(int carId);
    }
}
