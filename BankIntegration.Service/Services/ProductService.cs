using AutoMapper;
using BankIntegration.Domain.Entities;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.Model;

namespace BankIntegration.Service.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductResponseModel> GetProductById(long productId)
    {
        var result = await _unitOfWork.ProductRepository.GetById(productId);
        var responseModel = _mapper.Map<ProductResponseModel>(result);
        return responseModel;
    }

    public async Task<IEnumerable<ProductResponseModel>> GetAll()
    {
        var result = await _unitOfWork.ProductRepository.GetAll();
        var response = _mapper.Map<IEnumerable<ProductResponseModel>>(result);
        return response;
    }
}