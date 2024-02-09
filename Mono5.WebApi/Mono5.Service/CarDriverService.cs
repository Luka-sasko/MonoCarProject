using Mono5.Model;
using Mono5.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono5.Repository.Common;
namespace Mono5.Service
{
    public class CarDriverService : ICarDriverService
    {
        private readonly ICarDriverRepository CarDriverRepository;

        public CarDriverService(ICarDriverRepository repository)
        {
            CarDriverRepository = repository;
        }

        public Car FindCarById(int id)
        {
            return CarDriverRepository.FindCarById(id);
        }

        public Driver FindDriverById(int id)
        {
            return CarDriverRepository.FindDriverById(id);
        }

        public IEnumerable<Driver> GetAllCarsDrivers(int carId)
        {
            if (CarDriverRepository.FindCarById(carId) == null)
                return Enumerable.Empty<Driver>();
            return CarDriverRepository.GetAllCarsDrivers(carId);
        }

        public void AddCarDriver(int carId, int driverId)
        {
            if (CarDriverRepository.FindCarById(carId) != null && CarDriverRepository.FindDriverById(driverId) != null)
                CarDriverRepository.AddCarDriver(carId, driverId);
        }

        public void DeleteCarDriver(int driverId, int carId)
        {
            if (CarDriverRepository.FindCarById(carId) != null && CarDriverRepository.FindDriverById(driverId) != null)
                CarDriverRepository.DeleteCarDriver(driverId, carId);
        }

        public void UpdateCarDriver(int carId, int driverId, int newDriverId)
        {
            if (CarDriverRepository.FindCarById(carId) != null && CarDriverRepository.FindDriverById(driverId) != null)
                CarDriverRepository.UpdateCarDriver(carId, driverId, newDriverId);
        }

       
    }
}
