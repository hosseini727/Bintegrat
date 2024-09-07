using AutoMapper;
using BankIntegration.Infra.ElasticMapping;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Service.Model.BankInquiry;

namespace BankIntegration.Service.MappingProfile.BankMapping;


public class InquiryDepositProfile : Profile
{
    public InquiryDepositProfile()
    {
        CreateMap<ShebaInquiryResponseModel, ShebaInquiry>();
        CreateMap<ShebaInquiry, ShebaInquiryResponseModel>();
    }
}