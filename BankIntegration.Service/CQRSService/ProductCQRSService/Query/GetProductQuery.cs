using BankIntegration.Service.Model;
using MediatR;

namespace BankIntegration.Service.CQRSService.ProductCQRSService.Query;

public class GetProductQuery : IRequest<IEnumerable<ProductResponseModel>>
{
}