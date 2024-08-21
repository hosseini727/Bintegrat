using AutoMapper;
using BankIntegration.Infra.ElasticMapping;
using BankIntegration.Service.Model.BankInquiry;

namespace BankIntegration.Service.MappingProfile.BankMapping;

public class ShebaIndexProfile : Profile
{
    public ShebaIndexProfile()
    {
        CreateMap<ShebaInquiryResponseModel, ShebaInquiry>();
    }
}