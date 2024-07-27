using AutoMapper;
using BankIntegration.Service.Model.People.Request;
using BankIntegration.Service.Model.People.Response;
using SOS.Domain.Entities;


namespace BankIntegration.Service.MappingProfile.PeopleMapping;

public class GetPeopleResponseProfile : Profile
{
    public GetPeopleResponseProfile()
    {
        CreateMap<People,GetPeopleResponseModel>();
    }
}