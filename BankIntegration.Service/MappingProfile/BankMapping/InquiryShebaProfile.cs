using AutoMapper;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Service.Model.BankInquiry;

namespace BankIntegration.Service.MappingProfile.BankMapping;

public class InquiryShebaProfile : Profile
{
    public InquiryShebaProfile()
    {
        CreateMap<FinalResponseInquery, ShebaInquiryResponseModel>()
            .ForMember(des => des.IsSuccess,
                opt => opt.MapFrom(
                    src => src.AccountStatus == "02" ? true : false));

        CreateMap<IbanAccountOwner, AccountOwner>();
    }
}