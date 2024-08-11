using AutoMapper;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Service.Model.BankInquiry;

namespace BankIntegration.Service.MappingProfile.BankMapping;

public class InquiryShebaProfile : Profile
{
    public InquiryShebaProfile()
    {
        CreateMap<FinalResponseInquery, ShebaInquiryResponseModel>();

        CreateMap<IbanAccountOwner, AccountOwner>();
    }
}