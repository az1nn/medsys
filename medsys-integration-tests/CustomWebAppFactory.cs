using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using medsys.Data;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace medsys_integration_tests;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<UserContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                // Create open SqliteConnection so EF won't automatically close it.
                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=medsys_db;Username=postgres;Password=password123");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<UserContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseNpgsql(connection);
                });
            });

            builder.UseEnvironment("Development");
        }
    }
