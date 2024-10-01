using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Regiao.Infra.Contexts;
using Regiao.IntegrationTests.Fixtures;

namespace Regiao.IntegrationTests.Factories;

[CollectionDefinition(nameof(RegiaoFactoryCollection))]
public class RegiaoFactoryCollection : ICollectionFixture<RegiaoFactory>;

public class RegiaoFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private static readonly DockerFixture _dockerFixture = new();

    public async Task InitializeAsync()
    {
        await _dockerFixture.InitializeAsync();
        // ExecuteScript("create_table_regioes.sql");
        // ExecuteScript("create_table_cidades.sql");
        // ExecuteScript("create_table_contatos.sql");
        // ExecuteScript("insert_into_regioes_table.sql");
        // ExecuteScript("insert_into_cidades_table.sql");
        // ExecuteScript("insert_into_contatos_table.sql");
    }

    public new async Task DisposeAsync() => await _dockerFixture.DisposeAsync();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(DbContextOptions<ApplicationDbContext>);
            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(GetConnectionString(), x => x.MigrationsAssembly("Regiao.Infra"));
            });
        });
    }

    public string GetConnectionString()
    {
        return
            $"Server=localhost,{_dockerFixture.MsSqlContainer.GetMappedPublicPort(1433)};User=sa;Password=Strong_password_123!;TrustServerCertificate=True";
    }

    public void ExecuteScript(string scriptName)
    {
        var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "Sql", scriptName);
        string query = File.ReadAllText(scriptPath);

        using (var connection = new SqlConnection(GetConnectionString()))
        {
            connection.Open();
            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}