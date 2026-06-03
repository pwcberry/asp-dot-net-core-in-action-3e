using Microsoft.Extensions.DependencyInjection;
using MyLearning.Data.Sqlite;

namespace MyLearning.Services
{
    public enum MyLearningDatabase
    {
        Carsales,
        Movieland,
        Northwind
    }

    public static class ServiceCollectionExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddDatabaseService(MyLearningDatabase database)
            {
                switch (database)
                {
                    case MyLearningDatabase.Carsales:
                        services.AddDbContext<CarsalesContext>();
                        services.AddScoped<CarsalesService>();
                        break;
                    case MyLearningDatabase.Movieland:
                        services.AddDbContext<MovielandContext>();
                        services.AddScoped<MovielandService>();
                        break;
                    case MyLearningDatabase.Northwind:
                        services.AddDbContext<NorthwindContext>();
                        services.AddScoped<NorthwindService>();
                        break;
                }
                return services;
            }
        }
    }
}
