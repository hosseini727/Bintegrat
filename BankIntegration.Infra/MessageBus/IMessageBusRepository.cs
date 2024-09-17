namespace BankIntegration.Infra.MessageBus;

public interface IMessageBusRepository
{
    Task SendMessageAsync<T>(T message) where T : class;
    Task PublishEventAsync<T>(T message) where T : class;
}