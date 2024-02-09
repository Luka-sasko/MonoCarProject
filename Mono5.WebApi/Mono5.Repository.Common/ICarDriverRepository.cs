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

        Driver FindDriverById(int id);
        Car FindCarById(int id);
        void DeleteCarDriver(int driverId, int carId);
        int UpdateCarDriver(int carId, int driverId, int newDriverId);
        void AddCarDriver(int carId, int driverId);
        IEnumerable<Driver> GetAllCarsDrivers(int carId);
    }
}
