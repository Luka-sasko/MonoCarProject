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

        public async Task AddDriver(Driver driver)
        {
            if (driver == null)
                throw new ArgumentException("Driver cannot be null");
            await DriverRepository.AddDriver(driver);
        }

        public async Task DeleteDriver(int id)
        {
            var driver = await DriverRepository.FindDriverById(id);
            if (driver == null)
                throw new ArgumentException("Driver not found");
            await DriverRepository.DeleteDriver(id);
        }

        public async Task<IEnumerable<Driver>> GetAllDrivers()
        {
            return await DriverRepository.GetAllDrivers();
        }

        public async Task<Driver> GetDriverById(int id)
        {
            var driver = await DriverRepository.FindDriverById(id);
            if (driver == null)
                throw new ArgumentException("Driver not found");
            return driver;
        }

        public async Task UpdateDriver(int id, DriverUpdate editedDriver)
        {
            var driver = await DriverRepository.FindDriverById(id);
            if (driver == null)
                throw new ArgumentException("Driver not found");
            await DriverRepository.UpdateDriver(id, editedDriver);
        }
    }
}
