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
        Task<IEnumerable<Car>> GetAllCars();
        Task <Car> FindCarById(int id);
        Task DeleteCar(int id);
        Task UpdateCar(int id, CarUpdate updatedCar);
        Task AddCar(Car car);
    }
}
