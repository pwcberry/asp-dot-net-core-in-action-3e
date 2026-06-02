using Microsoft.Extensions.DependencyInjection;
using MyLearning.Data.Sqlite;

namespace MyLearning.Services
{
    public enum MyLearningDatabase
    {
        Northwind,
        Carsales
    }

    public static class ServiceCollectionExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddDatabaseService(MyLearningDatabase database)
            {
                switch (database)
                {
                    case MyLearningDatabase.Northwind:
                        services.AddDbContext<NorthwindContext>();
                        services.AddScoped<NorthwindService>();
                        break;
                    case MyLearningDatabase.Carsales:
                        //services.AddDbContext<CarsalesContext>();
                        services.AddScoped<CarsalesService>();
                        break;
                }
                return services;
            }
        }
    }
}
