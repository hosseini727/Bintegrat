using BankIntegration.Domain.Entities;
using BankIntegration.Infra.Repository.SQLRepository.Persistance;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;

namespace BankIntegration.Infra.Repository.SQLRepository.Repository;

public class ProductRepository : GenericRepository<NewPasargad_Product> , IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}