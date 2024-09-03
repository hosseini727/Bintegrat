using BankIntegration.Service.Model.BankInquiry;

namespace BankIntegration.Service.Contracts;

public interface IInquiryBankService
{
    Task<ShebaInquiryResponseModel> GetShebaInquiry(string accountNo);

    Task<ConvertAccountNoResponseModel> ConvertAccountNo(string accountNo);

    Task<FinalInquiryResponseModel> FinalInquiry(string accountNo);

    Task<IEnumerable<ConvertAccountNoResponseModel>> SearchConvertAccountNoInquiry(string accountNo);

}