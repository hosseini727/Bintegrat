using AutoMapper;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Service.CQRSService.ProductCQRSService.Query;
using BankIntegration.Service.Model;
using MediatR;

namespace BankIntegration.Service.CQRSService.ProductCQRSService.Handler;

public class GetProductHandler : IRequestHandler<GetProductQuery,IEnumerable<ProductResponseModel>>
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseModel>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ProductRepository.GetAll();
        var response = _mapper.Map<IEnumerable<ProductResponseModel>>(result);
        return response;
    }
}