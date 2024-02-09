using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private NpgsqlConnection connection;

        public IEnumerable<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();

            using (var cmd = new NpgsqlCommand())
            {
                connection = new NpgsqlConnection(CONNECTION_STRING);
                connection.Open();

                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM \"Car\"";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Car car = new Car(
                            (int)reader["Id"],
                            (string)reader["Model"],
                            (string)reader["Brand"],
                            (int)reader["ManufacturYear"]);

                        cars.Add(car);
                    }
                }
            }
            connection.Close();
            return cars;
        }
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

        public void UpdateCar(int id, CarUpdate editedCar)
        {
            Car car = FindCarById(id);

            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE \"Car\" SET \"Model\" = @model, \"Brand\" = @brand, \"ManufacturYear\" = @manufacturYear WHERE \"Id\" = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@model", editedCar.Model);
                    cmd.Parameters.AddWithValue("@brand", editedCar.Brand);
                    cmd.Parameters.AddWithValue("@manufacturYear", editedCar.ManufacturYear);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void DeleteCar(int id)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "DELETE FROM \"Car\" WHERE \"Id\" = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void AddCar(Car newCar)
        {
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO \"Car\" (\"Id\", \"Model\", \"Brand\", \"ManufacturYear\") VALUES (@id, @model, @brand, @manufacturYear)";
                    cmd.Parameters.AddWithValue("@id", newCar.Id);
                    cmd.Parameters.AddWithValue("@model", newCar.Model);
                    cmd.Parameters.AddWithValue("@brand", newCar.Brand);
                    cmd.Parameters.AddWithValue("@manufacturYear", newCar.ManufacturYear);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }


    }
}
