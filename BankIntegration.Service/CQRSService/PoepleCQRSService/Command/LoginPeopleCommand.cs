using BankIntegration.Service.Model.People.Request;
using BankIntegration.Service.Model.People.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.CQRSService.PoepleTransaction.Command
{
    public class LoginPeopleCommand : IRequest<loginPeopleResponseModel>
    {
        public loginPeopleRequestModel LoginPeopleModel { get; }

        public LoginPeopleCommand(loginPeopleRequestModel loginPeopleRequestModel)
        {
            LoginPeopleModel = loginPeopleRequestModel;
        }
    }
}
