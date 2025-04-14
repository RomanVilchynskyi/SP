using _08_TestDapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace _08_TestDapper.DbContext
{
    public interface ICarRepository
    {
        Car Create(Car car);
        void Delete(int id);
        Car GetCar(int id);
        List<Car> GetCars();
        void Update(Car car);
    }
}
