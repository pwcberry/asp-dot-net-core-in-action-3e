using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLearning.Data
{
    public static class ConfigurationExtensions
    {
        extension(IConfiguration configuration)
        {
            public string? GetSqliteConnection(string connectionName)
            {
                var connection = configuration[connectionName];

                if (string.IsNullOrEmpty(connection))
                {
                    connection = Environment.GetEnvironmentVariable($"SQLITE_{connectionName.ToUpper()}_CONNECTION");
                }

                return !string.IsNullOrEmpty(connection) ? connection : null;
            }
        }
    }
}
