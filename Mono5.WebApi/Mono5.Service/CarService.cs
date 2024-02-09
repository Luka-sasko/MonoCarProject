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
    public class CarService : ICarService
    {

        private readonly ICarRepository CarRepository;

        public CarService(ICarRepository carRepository)
        {
            CarRepository = carRepository;
        }


        public void AddCar(Car car)
        {
            if (car == null)
                throw new ArgumentException("Failed!");
            CarRepository.AddCar(car);
        }

        public void DeleteCar(int id)
        {
            var car = CarRepository.FindCarById(id);
            if (car == null)
                throw new ArgumentException("Failed!");
            CarRepository.DeleteCar(id);
        }

        public IEnumerable<Car> GetAllCars()
        {
            return CarRepository.GetAllCars();
        }

        public Car GetCarById(int id)
        {
            var car = CarRepository.FindCarById(id);
            if (car == null)
                throw new ArgumentException("Failed!");
            return car;
        }

        public void UpdateCar(int id, CarUpdate editedCar)
        {
            var car = CarRepository.FindCarById(id);
            if (car == null)
                throw new ArgumentException("Failed!");
            CarRepository.UpdateCar(id, editedCar);
        }
    }
}
