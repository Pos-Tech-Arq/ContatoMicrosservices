using Microsoft.Extensions.DependencyInjection;
using Regiao.Infra.Contexts;
using Regiao.IntegrationTests.Factories;

namespace Regiao.IntegrationTests;

[Collection(name: nameof(RegiaoFactoryCollection))]
public abstract class BaseIntegrationTests(RegiaoFactory factory)
{
    private readonly AsyncServiceScope _integrationTestScope = factory.Services.CreateAsyncScope();
    protected HttpClient Client => factory.Server.CreateClient();

    protected ApplicationDbContext DbContext =>
        _integrationTestScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
}