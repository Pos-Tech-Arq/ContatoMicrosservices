using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Regiao.Worker.Consumer;

namespace Regiao.Infra.Configurations;

public static class ConfigureRabbitExtension
{
    public static void ConfigureRabbit(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetSnakeCaseEndpointNameFormatter();
            busConfigurator.AddConsumer<ContatoCriadoConsumer>();

            busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
            {
                busFactoryConfigurator.Host("rabbitmq", hostConfigurator =>
                {
                    hostConfigurator.Username("guest");
                    hostConfigurator.Password("guest");
                });
                busFactoryConfigurator.UseJsonDeserializer();
                busFactoryConfigurator.ConfigureEndpoints(context);
            });
        });
    }
}