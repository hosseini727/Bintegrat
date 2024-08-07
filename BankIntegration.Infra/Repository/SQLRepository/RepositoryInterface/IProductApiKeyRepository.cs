using BankIntegration.Domain.Entities;

namespace BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;

public interface IProductApiKeyRepository : IGenericRepository<NewPasargad_ApiProductKey>
{
    Task<string> GetApikey(string productCode);
}