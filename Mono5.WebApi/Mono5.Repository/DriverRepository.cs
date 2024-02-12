using Mono5.Model;
using Mono5.Repository.Common;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mono5.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private const string CONNECTION_STRING = "Host=localhost:5432;" +
         "Username=postgres;" +
         "Password=postgres;" +
         "Database=postgres";

        public async Task<Driver> FindDriverById(int id)
        {
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
                            return new Driver(
                                (int)reader["Id"],
                                (string)reader["FirstName"],
                                (string)reader["LastName"],
                                (string)reader["Contact"]);
                        }
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<Driver>> GetAllDrivers()
        {
            var drivers = new List<Driver>();

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("SELECT * FROM \"Driver\"", connection))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        drivers.Add(new Driver(
                            (int)reader["Id"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (string)reader["Contact"]));
                    }
                }
            }

            return drivers;
        }

        public async Task AddDriver(Driver newDriver)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("INSERT INTO \"Driver\" (\"Id\", \"FirstName\", \"LastName\", \"Contact\") VALUES (@id, @firstName, @lastName, @contact)", connection))
                {
                    cmd.Parameters.AddWithValue("@id", newDriver.Id);
                    cmd.Parameters.AddWithValue("@firstName", newDriver.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", newDriver.LastName);
                    cmd.Parameters.AddWithValue("@contact", newDriver.Contact);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteDriver(int id)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("DELETE FROM \"Driver\" WHERE \"Id\" = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateDriver(int id, DriverUpdate updatedDriver)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("UPDATE \"Driver\" SET \"FirstName\" = @firstName, \"LastName\" = @lastName, \"Contact\" = @contact WHERE \"Id\" = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@firstName", updatedDriver.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", updatedDriver.LastName);
                    cmd.Parameters.AddWithValue("@Contact", updatedDriver.Contact);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
