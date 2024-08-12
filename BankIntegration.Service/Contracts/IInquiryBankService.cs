using BankIntegration.Service.Model.BankInquiry;

namespace BankIntegration.Service.Contracts;

public interface IInquiryBankService
{
    Task<ShebaInquiryResponseModel> GetShebaInquiry(string accountNo);

    Task<DepositInquiryResponseModel> GetDepositInquiry(string accountNo);

}