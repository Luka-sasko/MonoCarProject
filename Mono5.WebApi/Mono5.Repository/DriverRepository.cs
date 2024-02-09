using Mono5.Model;
using Mono5.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Mono5.Repository.Common;
using Mono5.Model;
namespace Mono5.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private const string CONNECTION_STRING = "Host=localhost:5432;" +
         "Username=postgres;" +
         "Password=postgres;" +
         "Database=postgres";

        private NpgsqlConnection connection;

        public Driver FindDriverById(int id)
        {
            {

                Driver driver = null;

                using (var cmd = new NpgsqlCommand())
                {
                    connection = new NpgsqlConnection(CONNECTION_STRING);
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM \"Driver\" WHERE \"Id\" = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            reader.Read();
                            driver = new Driver(

                                (int)reader["Id"],
                                (string)reader["FirstName"],
                                (string)reader["LastName"],
                                (string)reader["Contact"])
                            ;
                        }

                    }
                }
                connection.Close();
                return driver;
            }
        }
        public IEnumerable<Driver> GetAllDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            Driver driver = null;
            using (var cmd = new NpgsqlCommand())
            {
                connection = new NpgsqlConnection(CONNECTION_STRING);
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM \"Driver\"";
                using (var reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            driver = new Driver(
                                (int)reader["Id"],
                                (string)reader["FirstName"],
                                (string)reader["LastName"],
                                (string)reader["Contact"]);

                        drivers.Add(driver);
                    }

                }
            }
            connection.Close();
            return drivers;
        }



        public void AddDriver(Driver newDriver)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO \"Driver\" (\"Id\", \"FirstName\", \"LastNae\", \"Contact\") VALUES (@id, @firstName, @lastName, @contact)";
                    cmd.Parameters.AddWithValue("@id", newDriver.Id);
                    cmd.Parameters.AddWithValue("@firstName", newDriver.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", newDriver.LastName);
                    cmd.Parameters.AddWithValue("@contact", newDriver.Contact);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void DeleteDriver(int id)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "DELETE FROM \"Driver\" WHERE \"Id\" = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }



        public void UpdateDriver(int id, DriverUpdate updatedDriver)
        {
            Driver driver = FindDriverById(id);

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE \"Driver\" SET \"FirstName\" = @firstName, \"LastName\" = @lastName, \"Contact\" = @contact WHERE \"Id\" = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@firstName", updatedDriver.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", updatedDriver.LastName);
                    cmd.Parameters.AddWithValue("@Contact", updatedDriver.Contact);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }


    }
}
