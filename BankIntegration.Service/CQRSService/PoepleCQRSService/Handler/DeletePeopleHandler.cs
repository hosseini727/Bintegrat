using MediatR;
using AutoMapper;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Service.Model.People.Response;
using MediatR;
using SOS.Domain.Entities;
using BankIntegration.Service.CQRSService.PoepleTransaction.Command;

namespace BankIntegration.Service.CQRSService.PoepleTransaction.Handler;


public class DeletePeopleHandler : IRequestHandler<DeletePeopleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePeopleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeletePeopleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.PeopleRepository.Delete(request.PeopleId);

            await _unitOfWork.CompleteASync();

            return true;
        }
        catch (Exception ex)
        {
            return false; 
        }
    }

}