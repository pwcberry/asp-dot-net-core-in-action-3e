using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLearning.Data
{
    public static class ConfigurationExtensions
    {
        public static string? GetSqliteConnection(this IConfiguration configuration)
        {
            var path = configuration["DB_PATH"] as string;

            return !string.IsNullOrEmpty(path) ? $"Data Source={path}" : null;
        }
    }
}
