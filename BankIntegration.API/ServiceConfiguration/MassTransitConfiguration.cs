using MassTransit;

namespace BankIntegration.API.ServiceConfiguration;

public static class MassTransitConfiguration
{
    public static void AddMassTransitWithRabbitMq(this IServiceCollection service)
    {
        service.AddMassTransit((conf) =>
        {
            conf.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("admin");
                    h.Password("123qaz#@!~");
                });
            });
        });
    }
}