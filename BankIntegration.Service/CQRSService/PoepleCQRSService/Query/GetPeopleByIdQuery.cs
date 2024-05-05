using BankIntegration.Service.Model.People.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.CQRSService.PoepleTransaction.Query
{
    public class GetPeopleByIdQuery : IRequest<GetPeopleResponseModel>
    {
        public int PeopleId;

        public GetPeopleByIdQuery(int id)
        {
            PeopleId = id;
        }
    }
}
