using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi.BankModels;

namespace BankIntegration.Infra.ThirdApi.ConvertAccount;

public interface IConvertAccountNoBankHttp
{
    Task<ApiResponseModel<FinalResponseDepositInquery>> ConvertAccountNo(string depositNumber, string apiKey);
}