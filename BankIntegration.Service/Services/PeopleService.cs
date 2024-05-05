using BankIntegration.Service.Contracts;
using BankIntegration.Service.CQRSService.PoepleTransaction.Command;
using BankIntegration.Service.CQRSService.PoepleTransaction.Query;
using BankIntegration.Service.Model.People.Request;
using BankIntegration.Service.Model.People.Response;
using MediatR;


namespace BankIntegration.Service.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IMediator _mediator;


        public PeopleService(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<loginPeopleResponseModel> loginPeople(loginPeopleRequestModel model)
        {
            var command = new LoginPeopleCommand(model);
            var loginPeople = await _mediator.Send(command);
            return loginPeople;
        }

        public async Task<RefreshTokenPeopleResponseModel> RefreshToken(RefreshTokenPeopleRequestModel model)
        {
            var command = new RefreshTokenPeopleCommand(model);
            var refreshTokenPeople = await _mediator.Send(command);
            return refreshTokenPeople;
        }


        public async Task<AddPeopleResponseModel> AddPeople(AddPeopleRequestModel model)
        {
            var command = new AddPeopleCommand(model);
            var addedPeople = await _mediator.Send(command);
            return addedPeople;
        }

        public async Task<List<GetPeopleResponseModel>> GetPeople()
        {
            var query = new GetPeopleQuery();
            var result = await _mediator.Send(query);
            return result;
        }

        public async Task<GetPeopleResponseModel> UpdatePeople(UpdatePeopleRequestModel updateModel)
        {
            var command = new UpdatePeopleCommand(updateModel);
            var result = await _mediator.Send(command);
            return result;
        }

        public async Task<bool> DeletePeople(int id)
        {
            var command = new DeletePeopleCommand(id);
            var result = await _mediator.Send(command);
            return result;
        }

        public async Task<GetPeopleResponseModel> GetPeopleById(int peopleId)
        {
            var query = new GetPeopleByIdQuery(peopleId);
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
