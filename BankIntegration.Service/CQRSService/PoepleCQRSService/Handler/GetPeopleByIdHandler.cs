using AutoMapper;
using MediatR;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Service.Model.People.Response;

using BankIntegration.Service.CQRSService.PoepleTransaction.Query;

namespace BankIntegration.Service.CQRSService.PoepleTransaction.Handler;

public class GetPeopleByIdHandler : IRequestHandler<GetPeopleByIdQuery, GetPeopleResponseModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPeopleByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetPeopleResponseModel> Handle(GetPeopleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.PeopleRepository.GetById(request.PeopleId);
        if (result != null)
        {
            var people = _mapper.Map<GetPeopleResponseModel>(result);
            return people;
        }
        return new GetPeopleResponseModel
        {
            Message = "Person not found.", 
        };
    }
}