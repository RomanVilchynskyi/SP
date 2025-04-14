using _08_TestDapper.DbContext;
using _08_TestDapper.Model;
using System.Diagnostics;

namespace _08_TestDapper
{
    class Program
    {
        static Stat TestProvider(ICarRepository repos)
        {
            var stat = new Stat();
            Stopwatch sw;

            //Read
            sw = Stopwatch.StartNew();
            repos.GetCar(86);
            sw.Stop();
            stat.ReadByIdTime = sw.ElapsedMilliseconds;

            //ReadAll
            sw = Stopwatch.StartNew();
            repos.GetCars();
            sw.Stop();
            stat.ReadAllTime = sw.ElapsedMilliseconds;

            // Create
            sw = Stopwatch.StartNew();
            var car = repos.Create(new Car()
            {
                Make = "Ford",
                Model = "GT",
                ModelYear = 2007
            });

            sw.Stop();
            stat.CreateTime = sw.ElapsedMilliseconds;

            //Update
            sw = Stopwatch.StartNew();
            car.Model = "new Model";
            repos.Update(car);
            sw.Stop();
            stat.UpdateTime = sw.ElapsedMilliseconds;


            //Delete
            sw = Stopwatch.StartNew();
            repos.Delete(car.Id);
            sw.Stop();
            stat.DeleteTime = sw.ElapsedMilliseconds;

            foreach (var item in stat.GetType().GetProperties())
            {
                Console.WriteLine($"{item.Name} :: {item.GetValue(stat)} ms");
            }
            return stat;
        }
        static void Main(string[] args)
        {
            string conn = @"data source = DESKTOP-F5EBSVM\SQLEXPRESS; 
                            initial catalog = CarSalon; Integrated security = True; Connect Timeout = 2";

            Console.WriteLine("\n-------- Entity Framework Core ---------");
            TestProvider(new CarReposityEF(conn));
            Console.WriteLine("\n-------- ADO.Net -----------");
            TestProvider(new CarRepository_ADO_Net(conn));
            Console.WriteLine("\n-------- Dapper ----------");
            TestProvider(new CarRepositoryDapper(conn));

        }
    }
}
