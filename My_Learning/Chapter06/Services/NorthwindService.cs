using Microsoft.EntityFrameworkCore;
using MyLearning.Data.Sqlite;
using MyLearning.Data.Sqlite.Northwind;

namespace MyLearning.Chapter06.Services
{
    public class NorthwindService(NorthwindContext context)
    {
        public IList<Category> GetCategories() => context.Categories.ToList();
    }
}
