using AutoMapper;
using BankIntegration.Service.Model.People.Request;
using SOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.MappingProfile.PeopleMapping
{
    public class AddPeopleRequestProfile : Profile
    {
        public AddPeopleRequestProfile()
        {
            CreateMap<AddPeopleRequestModel, People>();
        }
    }
}
