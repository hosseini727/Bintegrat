using AutoMapper;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Service.CQRSService.ProductCQRSService.Query;
using BankIntegration.Service.Model;
using MediatR;

namespace BankIntegration.Service.CQRSService.ProductCQRSService.Handler;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery,ProductResponseModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductResponseModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ProductRepository.GetById(request.ProductId);
        var responseModel = _mapper.Map<ProductResponseModel>(result);
        return responseModel;
    }
}