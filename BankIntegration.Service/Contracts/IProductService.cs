using BankIntegration.Service.Model;

namespace BankIntegration.Service.Contracts;

public interface IProductService
{
    Task<ProductResponseModel> GetProductById(long productId);
    Task<IEnumerable<ProductResponseModel>> GetAll();
}