using MassTransit;

namespace BankIntegration.Infra.MessageBus;

public class MessageBusRepository(IPublishEndpoint bus) : IMessageBusRepository
{
    public async Task SendMessageAsync<T>(T message) where T : class
    {
        Task.Run(() => { });
        //await bus.Send(message);
    }

    public async Task PublishEventAsync<T>(T message) where T : class
    {
        await bus.Publish(message);
    }
}