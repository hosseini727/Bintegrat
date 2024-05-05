using BankIntegration.Service.Model.People.Request;
using BankIntegration.Service.Model.People.Response;
using MediatR;

namespace BankIntegration.Service.CQRSService.PoepleTransaction.Command;

public class UpdatePeopleCommand : IRequest<GetPeopleResponseModel>
{
    public UpdatePeopleRequestModel UpdateModel;

    public UpdatePeopleCommand(UpdatePeopleRequestModel updateModel)
    {
        UpdateModel = updateModel;
    }

}