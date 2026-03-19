using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using MyLearning.Data.Sqlite;
using MyLearning.Data.Sqlite.Northwind;

namespace MyLearning.Chapter06.Services
{
    public class NorthwindService(NorthwindContext context)
    {
        public IList<Category> GetCategories() => context.Categories.ToList();

        public IList<Employee> GetEmployees() => context.Employees.ToList();

        public Employee? GetEmployee(int id) => context.Employees.Find(id);

        public Territory? GetTerritoryByName(string name) => context.Territories.FirstOrDefault(t => t.TerritoryDescription!.ToLower() == name.ToLower());

        public IList<OrderSummary> GetOrdersByTerritory(string territoryId) {
            var query = from o in context.Orders
                        join e in context.Employees on o.EmployeeId equals e.Id
                        join et in context.EmployeeTerritories on e.Id equals et.EmployeeId
                        join t in context.Territories on et.TerritoryId equals t.Id
                        where t.Id == territoryId
                        select new OrderSummary(
                            OrderId: o.Id,
                            EmployeeId: o.EmployeeId,
                            ShippedDate: o.ShippedDate,
                            Territory: t.TerritoryDescription
                        );

            return query.ToList();
        }
    }

public record OrderSummary(int OrderId, int EmployeeId, DateOnly? ShippedDate, string? Territory);
}
