﻿using Mono5.Model;
using Mono5.Service.Common;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Car> FindCarById(int id)
        {
            return await CarDriverRepository.FindCarById(id);
        }

        public async Task<Driver> FindDriverById(int id)
        {
            return await CarDriverRepository.FindDriverById(id);
        }

        public async Task<IEnumerable<Driver>> GetAllCarsDrivers(int carId)
        {
            var car = await CarDriverRepository.FindCarById(carId);
            if (car == null)
                return Enumerable.Empty<Driver>();
            return await CarDriverRepository.GetAllCarsDrivers(carId);
        }

        public async Task AddCarDriver(int carId, int driverId)
        {
            var car = await CarDriverRepository.FindCarById(carId);
            var driver = await CarDriverRepository.FindDriverById(driverId);
            if (car != null && driver != null)
                await CarDriverRepository.AddCarDriver(carId, driverId);
        }

        public async Task DeleteCarDriver(int driverId, int carId)
        {
            var car = await CarDriverRepository.FindCarById(carId);
            var driver = await CarDriverRepository.FindDriverById(driverId);
            if (car != null && driver != null)
                await CarDriverRepository.DeleteCarDriver(driverId, carId);
        }

        public async Task UpdateCarDriver(int carId, int driverId, int newDriverId)
        {
            var car = await CarDriverRepository.FindCarById(carId);
            var driver = await CarDriverRepository.FindDriverById(driverId);
            if (car != null && driver != null)
                await CarDriverRepository.UpdateCarDriver(carId, driverId, newDriverId);
        }
    }
}
