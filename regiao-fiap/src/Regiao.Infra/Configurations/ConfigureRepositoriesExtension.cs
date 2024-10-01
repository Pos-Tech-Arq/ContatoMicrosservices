using Microsoft.Extensions.DependencyInjection;
using Regiao.Domain.Contracts;
using Regiao.Infra.Repositories;

namespace Regiao.Infra.Configurations;

public static class ConfigureRepositoriesExtension
{
    public static void ConfigureRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRegiaoRepository, RegiaoRepository>();
    }
}