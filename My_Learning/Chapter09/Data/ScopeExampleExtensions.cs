namespace MyLearning.Chapter09.Data;

public static class ScopeExampleExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddScopedExamples(LifetimeDefinitions lifetimes, IEnumerator<int> numbers)
        {
            switch (lifetimes)
            {
                case LifetimeDefinitions.AllSingleton:
                    services.AddSingleton(_ => new DataContext(numbers));
                    services.AddSingleton<Repository>();
                    break;
                case LifetimeDefinitions.AllScoped:
                    services.AddScoped(_ => new DataContext(numbers));
                    services.AddScoped<Repository>();
                    break;
                case LifetimeDefinitions.DataContextScopedOnly:
                    services.AddScoped(_ => new DataContext(numbers));
                    services.AddSingleton<Repository>();
                    break;
                case LifetimeDefinitions.RepositoryScopedOnly:
                    services.AddSingleton(_ => new DataContext(numbers));
                    services.AddScoped<Repository>();
                    break;
                case LifetimeDefinitions.AllTransient:
                default:
                    services.AddTransient(_ => new DataContext(numbers));
                    services.AddTransient<Repository>();
                    break;
            }
            return services;
        }
    }
}

public enum LifetimeDefinitions
{
    AllSingleton,
    AllScoped,
    AllTransient,
    DataContextScopedOnly,
    RepositoryScopedOnly
}