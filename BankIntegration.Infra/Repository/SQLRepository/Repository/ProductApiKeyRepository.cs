using BankIntegration.Domain.Entities;
using BankIntegration.Infra.Persistance;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace BankIntegration.Infra.Repository.SQLRepository.Repository;

public class ProductApiKeyRepository : GenericRepository<NewPasargad_ApiProductKey>, IProductApiKeyRepository
{
    public ProductApiKeyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<string> GetApikey(string productCode)
    {
        var result = await _dbSet.Where(x =>
            x.NewPasargad_Product.ProductCode == productCode && x.IsActive == true).FirstOrDefaultAsync();
        if (result != null)
            return result.ApiKey;
        return null;
    }
}