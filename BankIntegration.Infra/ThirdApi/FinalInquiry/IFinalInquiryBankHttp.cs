using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi.BankModels;

namespace BankIntegration.Infra.ThirdApi.ConvertAccount;

public interface IFinalInquiryBankHttp
{
    Task<ApiResponseModel<FinalResponseInquiry>> FinalInquiry(string depositNumber, string apiKey);
}