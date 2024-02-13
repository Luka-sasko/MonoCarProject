using Mono5.Common;
using Mono5.Model;
using Mono5.Repository.Common;
using Npgsql;
using System.Collections.Generic;
using System.Text;
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

        public async Task<IEnumerable<Driver>> GetDrivers(Paging paging, Sorting sorting, DriverFiltering driverFilter)
        {
            var drivers = new List<Driver>();
            if (driverFilter != null)
            {
                using (var connection = new NpgsqlConnection(CONNECTION_STRING))
                {
                    await connection.OpenAsync();

                    StringBuilder queryBuilder = new StringBuilder("SELECT * FROM \"Driver\" WHERE 1 = 1");

                    using (var cmd = new NpgsqlCommand(queryBuilder.ToString(), connection))
                    {
                        if (!string.IsNullOrEmpty(driverFilter.FirstName))
                        {
                            queryBuilder.Append(" AND \"FirstName\" = @FirstName");
                            cmd.Parameters.AddWithValue("@FirstName", driverFilter.FirstName);
                        }

                        if (!string.IsNullOrEmpty(driverFilter.LastName))
                        {
                            queryBuilder.Append(" AND \"LastName\" = @LastName");
                            cmd.Parameters.AddWithValue("@LastName", driverFilter.LastName);
                        }

                        if (!string.IsNullOrEmpty(driverFilter.Contact))
                        {
                            queryBuilder.Append(" AND \"Contact\" = @Contact");
                            cmd.Parameters.AddWithValue("@Contact", driverFilter.Contact);
                        }

                        queryBuilder.Append($" ORDER BY \"{sorting.SortBy}\" {(sorting.IsAsc ? "ASC" : "DESC")}");
                        queryBuilder.Append(" LIMIT @PageSize OFFSET @OffSetValue");

                        if (!string.IsNullOrEmpty(sorting.SortBy))
                        {
                            cmd.Parameters.AddWithValue("@SortBy", sorting.SortBy);
                        }

                        cmd.Parameters.AddWithValue("@PageSize", paging.PageSize);
                        cmd.Parameters.AddWithValue("@OffSetValue", (paging.PageNumber - 1) * paging.PageSize);


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
                }
            }
            return drivers;
        }


        public async Task<Driver> AddDriver(Driver newDriver)
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
            return await FindDriverById(newDriver.Id);
        }

        public async Task<Driver> DeleteDriver(int id)
        {
            var driver = await FindDriverById(id);
            using (var connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var cmd = new NpgsqlCommand("DELETE FROM \"Driver\" WHERE \"Id\" = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return driver;
        }

        public async Task<Driver> UpdateDriver(int id, DriverUpdate updatedDriver)
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
            return await FindDriverById(id);
        }



        /*
        * public async Task<IEnumerable<Driver>> GetDrivers(DriverFilter driverFilter)
       {
           var drivers = new List<Driver>();

           using (var connection = new NpgsqlConnection(CONNECTION_STRING))
           {
               await connection.OpenAsync();

               StringBuilder queryBuilder = new StringBuilder("SELECT * FROM \"Driver\"");

               if (driverFilter != null && (!string.IsNullOrEmpty(driverFilter.FirstName) || !string.IsNullOrEmpty(driverFilter.LastName) || !string.IsNullOrEmpty(driverFilter.Contact)))
               {
                   queryBuilder.Append(" WHERE 1 = 1");

                   if (!string.IsNullOrEmpty(driverFilter.FirstName))
                   {
                       queryBuilder.Append(" AND \"FirstName\" = @FirstName");
                   }

                   if (!string.IsNullOrEmpty(driverFilter.LastName))
                   {
                       queryBuilder.Append(" AND \"LastName\" = @LastName");
                   }

                   if (!string.IsNullOrEmpty(driverFilter.Contact))
                   {
                       queryBuilder.Append(" AND \"Contact\" = @Contact");
                   }
               }

               using (var cmd = new NpgsqlCommand(queryBuilder.ToString(), connection))
               {
                   if (driverFilter != null && (!string.IsNullOrEmpty(driverFilter.FirstName) || !string.IsNullOrEmpty(driverFilter.LastName) || !string.IsNullOrEmpty(driverFilter.Contact)))
                   {
                       if (!string.IsNullOrEmpty(driverFilter.FirstName))
                       {
                           cmd.Parameters.AddWithValue("@FirstName", driverFilter.FirstName);
                       }

                       if (!string.IsNullOrEmpty(driverFilter.LastName))
                       {
                           cmd.Parameters.AddWithValue("@LastName", driverFilter.LastName);
                       }

                       if (!string.IsNullOrEmpty(driverFilter.Contact))
                       {
                           cmd.Parameters.AddWithValue("@Contact", driverFilter.Contact);
                       }
                   }

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
           }

           return drivers;
       }
       */
    }
}
