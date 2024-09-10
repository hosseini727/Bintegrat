using MassTransit;

namespace BankIntegration.API.ServiceConfiguration;

public static class MassTransitConfiguration
{
    public static void AddMassTransitWithRabbitMq(this IServiceCollection service)
    {
        service.AddMassTransit(
            x => x.UsingRabbitMq((context, cfg) => { cfg.Host("rabbitmq://localhost:5601"); })
        );
    }
}