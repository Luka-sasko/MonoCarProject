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
        Task<Car> GetCarById(int id);
        Task<IEnumerable<Car>> GetAllCars();
        Task AddCar(Car car);
        Task UpdateCar(int id, CarUpdate editedCar);
        Task DeleteCar(int id);
    }
}
