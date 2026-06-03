using MyLearning.Data.Sqlite;
using MyLearning.Data.Sqlite.Carsales;

namespace MyLearning.Services
{
    public class CarsalesService(CarsalesContext context)
    {
        public IList<Models> GetAllModels() => context.Models.ToList();
    }
}
