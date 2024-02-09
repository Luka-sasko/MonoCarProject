using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono5.Model;
namespace Mono5.Service.Common
{
    public interface ICarService
    {
        Car GetCarById(int id);
        IEnumerable<Car> GetAllCars();
        void AddCar(Car car);
        void UpdateCar(int id, CarUpdate editedCar);
        void DeleteCar(int id);
    }
}
