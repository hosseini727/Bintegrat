using BankIntegration.Service.Model;
using MediatR;

namespace BankIntegration.Service.CQRSService.ProductCQRSService.Query;

public class GetProductByIdQuery : IRequest<ProductResponseModel>
{
    public long ProductId { get; }

    public GetProductByIdQuery(long productId)
    {
        ProductId = productId;
    }
}