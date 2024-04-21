namespace BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    IProductApiKeyRepository ProductApiKeyRepository { get; }
    Task<bool> CompleteASync();
}