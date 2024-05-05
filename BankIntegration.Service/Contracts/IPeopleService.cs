using BankIntegration.Service.Model.People.Request;
using BankIntegration.Service.Model.People.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.Contracts
{
    public interface IPeopleService
    {
        Task<RefreshTokenPeopleResponseModel> RefreshToken(RefreshTokenPeopleRequestModel model);
        Task<loginPeopleResponseModel> loginPeople(loginPeopleRequestModel model);
        Task<AddPeopleResponseModel> AddPeople(AddPeopleRequestModel model);
        Task<List<GetPeopleResponseModel>> GetPeople();
        Task<GetPeopleResponseModel> UpdatePeople(UpdatePeopleRequestModel updateModel);
        Task<bool> DeletePeople(int id);
        Task<GetPeopleResponseModel> GetPeopleById(int id);
    }
}
