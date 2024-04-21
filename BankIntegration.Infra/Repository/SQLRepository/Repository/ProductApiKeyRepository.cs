using BankIntegration.Domain.Entities;
using BankIntegration.Infra.Persistance;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace BankIntegration.Infra.Repository.SQLRepository.Repository;

public class ProductApiKeyRepository :  GenericRepository<NewPasargad_ApiProductKey>,IProductApiKeyRepository
{
    public ProductApiKeyRepository(ApplicationDbContext context) : base(context)
    {
    }
}