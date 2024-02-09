using Mono5.Model;
using Mono5.Repository.Common;
using Mono5.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono5.Service
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository DriverRepository;
        public DriverService(IDriverRepository driverRepository)
        {
            DriverRepository = driverRepository;
        }
        public void AddDriver(Driver driver)
        {
            if (driver == null)
                throw new ArgumentException("Failed!");
            DriverRepository.AddDriver(driver);
        }

        public void DeleteDriver(int id)
        {

            var driver = DriverRepository.FindDriverById(id);
            if (driver == null)
                throw new ArgumentException("Failed!");
            DriverRepository.DeleteDriver(id);
        }

        public IEnumerable<Driver> GetAllDrivers()
        {
            return DriverRepository.GetAllDrivers();
        }

        public Driver GetDriverById(int id)
        {
            var driver = DriverRepository.FindDriverById(id);
            if (driver == null)
                throw new ArgumentException("Failed!");
            return driver;
        }

        public void UpdateDriver(int id, DriverUpdate editedDriver)
        {
            var driver = DriverRepository.FindDriverById(id);
            if (driver == null)
                throw new ArgumentException("Failed!");
            DriverRepository.UpdateDriver(id, editedDriver);
        }
    }
}
