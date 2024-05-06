using SOS.Infrastructure.Repository.Interface;

namespace BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    IProductApiKeyRepository ProductApiKeyRepository { get; }
    IPeopleRepository PeopleRepository { get; }

    Task<bool> CompleteASync();
}