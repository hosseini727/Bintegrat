using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi.BankModels;

namespace BankIntegration.Infra.ThirdApi.Sheba;

public interface IInquiryBankHttp
{
    Task<ApiResponseModel<FinalResponseInquery>> GetSebaInquiry(string accountNumber , string apiKey);
}