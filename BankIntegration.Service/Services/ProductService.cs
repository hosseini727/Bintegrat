using AutoMapper;
using BankIntegration.Domain.Entities;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.CQRSService.ProductCQRSService.Query;
using BankIntegration.Service.Model;
using MediatR;

namespace BankIntegration.Service.Services;

public class ProductService : IProductService
{
    private readonly IMediator _mediator;

    public ProductService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ProductResponseModel> GetProductById(long productId)
    {
        var query = new GetProductByIdQuery(productId);
        var result = await _mediator.Send(query);
        return result;
    }

    public async Task<IEnumerable<ProductResponseModel>> GetAll()
    {
        var query = new GetProductQuery();
        var result = await _mediator.Send(query);
        return result;
    }
}