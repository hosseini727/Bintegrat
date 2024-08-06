using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi.BankModels;

namespace BankIntegration.Infra.ThirdApi;

public interface IBankHttp
{
    Task<ApiResponseModel<FinalResponseInquery>> GetSebaInquiry(string accountNumber);
}