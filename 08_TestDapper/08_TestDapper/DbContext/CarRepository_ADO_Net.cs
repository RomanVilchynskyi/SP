using _08_TestDapper.Model;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace _08_TestDapper.DbContext
{
    public class CarRepository_ADO_Net : ICarRepository
    {
        string connectionString;
        public CarRepository_ADO_Net(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Car Create(Car car)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string cmdText = $"insert into Cars(Make, Model, ModelYear) values (@Make, @Model, @ModelYear); select cast(scope_identity() as int)";
                SqlCommand command = new SqlCommand(cmdText, conn);
                command.Parameters.Add("@Make", System.Data.SqlDbType.NVarChar).Value = car.Make;
                command.Parameters.Add("@Model", System.Data.SqlDbType.NVarChar).Value = car.Model;
                command.Parameters.Add("@ModelYear", System.Data.SqlDbType.NVarChar).Value = car.ModelYear;

                int carId = (int)command.ExecuteScalar();
                car.Id = carId;
                return car;
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string cmdText = $"delete from Cars where Id = @id";
                SqlCommand command = new SqlCommand(cmdText, conn);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                command.ExecuteNonQuery();
            }
        }

        public Car GetCar(int id)
        {
            Car car = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string cmdText = $"select* from Cars where Id = @id";
                SqlCommand command = new SqlCommand(cmdText, conn);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    car = new Car()
                    {
                        Id = reader.GetInt32(0),
                        Make = reader.GetString(1),
                        Model = reader.GetString(2),
                        ModelYear = reader.GetInt32(3)
                    };

                }
                reader.Close();
            }
            return car;
        }

        public List<Car> GetCars()
        {
            List<Car> cars = new List<Car>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string cmdText = $"select* from Cars";
                SqlCommand command = new SqlCommand(cmdText, conn);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cars.Add(new Car()
                    {
                        Id = reader.GetInt32(0),
                        Make = reader.GetString(1),
                        Model = reader.GetString(2),
                        ModelYear = reader.GetInt32(3)
                    });

                }
                reader.Close();
            }
            return cars;
        }

        public void Update(Car car)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string cmdText = $"update Cars set Make = @Make, Model = @Model, ModelYear = @ModelYear where Id = @Id";
                SqlCommand command = new SqlCommand(cmdText, conn);
                command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar).Value = car.Id;
                command.Parameters.Add("@Make", System.Data.SqlDbType.NVarChar).Value = car.Make;
                command.Parameters.Add("@Model", System.Data.SqlDbType.NVarChar).Value = car.Model;
                command.Parameters.Add("@ModelYear", System.Data.SqlDbType.NVarChar).Value = car.ModelYear;

                command.ExecuteNonQuery();
            }
        }
    }
}
