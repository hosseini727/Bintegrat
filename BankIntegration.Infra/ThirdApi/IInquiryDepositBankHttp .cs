using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi.BankModels;

namespace BankIntegration.Infra.ThirdApi;

public interface IInquiryDepositBankHttp
{
    Task<ApiResponseModel<FinalResponseDepositInquery>> GetDepositInquiry(string depositNumber, string apiKey);
}