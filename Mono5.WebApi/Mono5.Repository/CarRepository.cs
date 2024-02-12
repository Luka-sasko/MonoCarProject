using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using Mono5.Model;
using Mono5.Repository.Common;

namespace Mono5.Repository
{
    public class CarRepository : ICarRepository
    {
        private const string CONNECTION_STRING = "Host=localhost:5432;" +
         "Username=postgres;" +
         "Password=postgres;" +
         "Database=postgres";

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            var cars = new List<Car>();

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("SELECT * FROM \"Car\"", connection))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cars.Add(new Car(
                            (int)reader["Id"],
                            (string)reader["Model"],
                            (string)reader["Brand"],
                            (int)reader["ManufacturYear"]));
                    }
                }
            }

            return cars;
        }

        public async Task<Car> FindCarById(int id)
        {
            Car car = null;

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("SELECT * FROM \"Car\" WHERE \"Id\" = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            car = new Car(
                                (int)reader["Id"],
                                (string)reader["Model"],
                                (string)reader["Brand"],
                                (int)reader["ManufacturYear"]);
                        }
                    }
                }
            }

            return car;
        }

        public async Task UpdateCar(int id, CarUpdate editedCar)
        {
            Car car = await FindCarById(id);

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("UPDATE \"Car\" SET \"Model\" = @model, \"Brand\" = @brand, \"ManufacturYear\" = @manufacturYear WHERE \"Id\" = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@model", editedCar.Model);
                    cmd.Parameters.AddWithValue("@brand", editedCar.Brand);
                    cmd.Parameters.AddWithValue("@manufacturYear", editedCar.ManufacturYear);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteCar(int id)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("DELETE FROM \"Car\" WHERE \"Id\" = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddCar(Car newCar)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("INSERT INTO \"Car\" (\"Id\", \"Model\", \"Brand\", \"ManufacturYear\") VALUES (@id, @model, @brand, @manufacturYear)", connection))
                {
                    cmd.Parameters.AddWithValue("@id", newCar.Id);
                    cmd.Parameters.AddWithValue("@model", newCar.Model);
                    cmd.Parameters.AddWithValue("@brand", newCar.Brand);
                    cmd.Parameters.AddWithValue("@manufacturYear", newCar.ManufacturYear);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
