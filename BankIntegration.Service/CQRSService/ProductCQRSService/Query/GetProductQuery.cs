using BankIntegration.Service.Model;
using MediatR;

namespace BankIntegration.Service.CQRSService.ProductCQRSService.Query;

public record GetProductQuery : IRequest<IEnumerable<ProductResponseModel>>
{
}