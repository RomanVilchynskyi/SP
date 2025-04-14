using _08_TestDapper.Model;
using Dapper;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace _08_TestDapper.DbContext
{
    public class CarRepositoryDapper : ICarRepository
    {
        string connectionString = null;
        public CarRepositoryDapper(string conn)
        {
            connectionString = conn;
        }
        public Car Create(Car car)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                /*var sqlQuery = $"insert into Cars(Make, Model, ModelYear) values (@Make, @Model, @ModelYear)";
                db.Execute(sqlQuery, car);*/
                string cmdText = $"insert into Cars(Make, Model, ModelYear) values (@Make, @Model, @ModelYear); select cast(scope_identity() as int)";
                int carId = db.Query<int>(cmdText, car).FirstOrDefault();


                car.Id = carId;
                return car;
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string cmdText = $"delete from Cars where Id = @id";

                db.Execute(cmdText, new { id });
            }
        }

        public Car GetCar(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string cmdText = $"select* from Cars where Id = @id";

                return db.QueryFirstOrDefault<Car>(cmdText, new { id });
            }
        }

        public List<Car> GetCars()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Car>("select* from Cars").ToList();
            }
        }

        public void Update(Car car)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string cmdText = $"update Cars set Make = @Make, Model = @Model, ModelYear = @ModelYear where Id = @Id";
                db.Execute(cmdText, car);
            }
        }
    }
}
