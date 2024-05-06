using AutoMapper;
using MediatR;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Service.Model.People.Response;

using BankIntegration.Service.CQRSService.PoepleTransaction.Query;

namespace BankIntegration.Service.CQRSService.PoepleTransaction.Handler;

public class GetPeopleHandler : IRequestHandler<GetPeopleQuery, List<GetPeopleResponseModel>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPeopleHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<List<GetPeopleResponseModel>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.PeopleRepository.GetAll();
        var response = _mapper.Map<List<GetPeopleResponseModel>>(result);
        return response;
    }
}