using BankIntegration.Service.Model.People.Request;
using BankIntegration.Service.Model.People.Response;
using MediatR;


namespace BankIntegration.Service.CQRSService.PoepleTransaction.Command
{
    public class RefreshTokenPeopleCommand : IRequest<RefreshTokenPeopleResponseModel>
    {
        public RefreshTokenPeopleRequestModel RefreshTokenPeopleModel { get; }
        public RefreshTokenPeopleCommand(RefreshTokenPeopleRequestModel RefreshTokenPeopleResponseModel)
        {
            RefreshTokenPeopleModel = RefreshTokenPeopleResponseModel;
        }
    }
}
