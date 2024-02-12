using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using Mono5.Model;
using Mono5.Repository.Common;

namespace Mono5.Repository
{
    public class CarDriverRepository : ICarDriverRepository
    {
        private const string CONNECTION_STRING = "Host=localhost:5432;" +
         "Username=postgres;" +
         "Password=postgres;" +
         "Database=postgres";

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

        public async Task<Driver> FindDriverById(int id)
        {
            Driver driver = null;

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("SELECT * FROM \"Driver\" WHERE \"Id\" = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            driver = new Driver(
                                (int)reader["Id"],
                                (string)reader["FirstName"],
                                (string)reader["LastName"],
                                (string)reader["Contact"]);
                        }
                    }
                }
            }

            return driver;
        }

        public async Task<IEnumerable<Driver>> GetAllCarsDrivers(int carId)
        {
            var drivers = new List<Driver>();

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("SELECT * FROM \"Driver\" d INNER JOIN \"CarDriver\" cd ON d.\"Id\" = cd.\"DriverId\" WHERE cd.\"CarId\" = @carId", connection))
                {
                    cmd.Parameters.AddWithValue("@carId", carId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var driver = new Driver(
                                (int)reader["Id"],
                                (string)reader["FirstName"],
                                (string)reader["LastName"],
                                (string)reader["Contact"]);

                            drivers.Add(driver);
                        }
                    }
                }
            }

            return drivers;
        }

        public async Task AddCarDriver(int carId, int driverId)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                var driver = await FindDriverById(driverId);
                var car = await FindCarById(carId);

                using (var cmd = new NpgsqlCommand("INSERT INTO \"CarDriver\"(\"DriverId\", \"CarId\") VALUES (@driverId, @carId)", connection))
                {
                    cmd.Parameters.AddWithValue("@driverId", driverId);
                    cmd.Parameters.AddWithValue("@carId", carId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteCarDriver(int driverId, int carId)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("DELETE FROM \"CarDriver\" WHERE \"DriverId\" = @driverId AND \"CarId\" = @carId", connection))
                {
                    cmd.Parameters.AddWithValue("@driverId", driverId);
                    cmd.Parameters.AddWithValue("@carId", carId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> UpdateCarDriver(int driverId, int carId, int newDriverId)
        {
            int numOfAffectedRows = 0;

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("UPDATE \"CarDriver\" SET \"DriverId\" = @newDriverId WHERE \"DriverId\" = @driverId AND \"CarId\" = @carId", connection))
                {
                    cmd.Parameters.AddWithValue("@newDriverId", newDriverId);
                    cmd.Parameters.AddWithValue("@driverId", driverId);
                    cmd.Parameters.AddWithValue("@carId", carId);

                    numOfAffectedRows = await cmd.ExecuteNonQueryAsync();
                }
            }

            return numOfAffectedRows;
        }
    }
}
