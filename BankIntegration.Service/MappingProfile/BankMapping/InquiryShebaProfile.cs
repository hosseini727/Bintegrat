using AutoMapper;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Service.Model.BankInquiry;

namespace BankIntegration.Service.MappingProfile.BankMapping;

public class InquiryShebaProfile : Profile
{
    public InquiryShebaProfile()
    {
        CreateMap<FinalResponseInquery, ShebaInquiryResponseModel>()
            .ForMember(des => des.IsActive,
                opt => opt.MapFrom(src => src.IsSuccess));

        CreateMap<IbanAccountOwner, AccountOwner>();
    }
}