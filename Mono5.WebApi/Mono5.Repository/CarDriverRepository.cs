using Mono5.Model;
using Mono5.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono5.Model;
using Mono5.Repository.Common;
using Npgsql;

namespace Mono5.Repository
{
    public class CarDriverRepository : ICarDriverRepository
    {

        private const string CONNECTION_STRING = "Host=localhost:5432;" +
         "Username=postgres;" +
         "Password=postgres;" +
         "Database=postgres";

        private NpgsqlConnection connection;

        public Car FindCarById(int id)
        {
            Car car = null;

            using (var cmd = new NpgsqlCommand())
            {
                connection = new NpgsqlConnection(CONNECTION_STRING);
                connection.Open();

                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM \"Car\" WHERE \"Id\" = @id";
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        car = new Car(
                            (int)reader["Id"],
                            (string)reader["Model"],
                            (string)reader["Brand"],
                            (int)reader["ManufacturYear"]);
                    }
                }
            }
            connection.Close();
            return car;
        }

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

        IEnumerable<Driver> ICarDriverRepository.GetAllCarsDrivers(int carId)
        {
            List<Driver> drivers = new List<Driver>();

            using (var cmd = new NpgsqlCommand())
            {
                connection = new NpgsqlConnection(CONNECTION_STRING);
                connection.Open();

                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM \"Driver\" d INNER JOIN \"CarDriver\" cd ON d.\"Id\" = cd.\"DriverId\" WHERE cd.\"CarId\" = @carId";
                cmd.Parameters.AddWithValue("@carId", carId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var driver = new Driver
                        (
                            (int)reader["Id"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (string)reader["Contact"]
                        );
                        drivers.Add(driver);
                    }
                }
            }
            connection.Close();
            return drivers;
        }



        void ICarDriverRepository.AddCarDriver(int carId, int driverId)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                var driver = FindDriverById(driverId);
                var car = FindCarById(carId);

                using (var cmd = new NpgsqlCommand())
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO \"CarDriver\"(\"DriverId\", \"CarId\") VALUES (@driverId, @carId)";
                    cmd.Parameters.AddWithValue("@driverId", driverId);
                    cmd.Parameters.AddWithValue("@carId", carId);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        void ICarDriverRepository.DeleteCarDriver(int driverId, int carId)
        {

            using (var cmd = new NpgsqlCommand())
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = "DELETE FROM \"CarDriver\" WHERE \"DriverId\" = @driverId AND \"CarId\" = @carId";
                cmd.Parameters.AddWithValue("@driverId", driverId);
                cmd.Parameters.AddWithValue("@carId", carId);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public int UpdateCarDriver(int driverId, int carId, int newDriverId)
        {
            int numOfAffectedRows = 0;

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE \"CarDriver\" SET \"DriverId\" = @newDriverId WHERE \"DriverId\" = @driverId AND \"CarId\" = @carId";
                    cmd.Parameters.AddWithValue("@newDriverId", newDriverId);
                    cmd.Parameters.AddWithValue("@driverId", driverId);
                    cmd.Parameters.AddWithValue("@carId", carId);

                    numOfAffectedRows = cmd.ExecuteNonQuery();
                }
            }

            return numOfAffectedRows;
        }
    }
}
