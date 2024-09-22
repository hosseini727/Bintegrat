using BankIntegration.Service.Model.People.Request;
using BankIntegration.Service.Model.People.Response;
using MediatR;

namespace BankIntegration.Service.CQRSService.PoepleTransaction.Command;

public record AddPeopleCommand : IRequest<AddPeopleResponseModel>
{
    public AddPeopleRequestModel AddPeopleModel { get; }

    public AddPeopleCommand(AddPeopleRequestModel addPeopleRequestModel)
    {
        AddPeopleModel = addPeopleRequestModel;
    }
}