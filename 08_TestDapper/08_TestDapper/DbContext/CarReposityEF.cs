using _08_TestDapper.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace _08_TestDapper.DbContext
{
    public class CarDbModel : Microsoft.EntityFrameworkCore.DbContext
    {
        string connectionString = null;
        public CarDbModel(string connectionstring)
        {
            this.connectionString = connectionstring;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@$"{connectionString};
                                            Trust Server Certificate = True;
                                            Application Intent = ReadWrite;
                                            Multi Subnet Failover = False;");
        }
        public DbSet<Car> Cars { get; set; }
    }
    public class CarReposityEF : ICarRepository
    {
        CarDbModel context = null;

        public CarReposityEF(string conn)
        {
            context = new CarDbModel(conn);
        }
        public Car Create(Car car)
        {
            var added = context.Cars.Add(car);
            context.SaveChanges();
            return added.Entity;
        }

        public void Delete(int id)
        {
            var car = context.Cars.Find(id);
            if (car != null)
            {
                context.Cars.Remove(car);
                context.SaveChanges();
            }
        }

        public Car GetCar(int id)
        {
            return context.Cars.Find(id);
        }

        public List<Car> GetCars()
        {
            return context.Cars.ToList();
        }

        public void Update(Car car)
        {
            context.Entry(car).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
