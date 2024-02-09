using Mono5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono5.Repository.Common
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAllCars();
        Car FindCarById(int id);
        void DeleteCar(int id);
        void UpdateCar(int id, CarUpdate updatedCar);
        void AddCar(Car car);
    }
}
