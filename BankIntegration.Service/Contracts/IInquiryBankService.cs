using BankIntegration.Service.Model.BankInquiry;

namespace BankIntegration.Service.Contracts;

public interface IInquiryBankService
{
    Task<ShebaInquiryResponseModel> GetShebaInquiry(string accountNo);

    Task<ConvertAccountNoResponseModel> ConvertAccountNo(string accountNo);

    Task<FinalInquiryResponseModel> FinalInquiry(string accountNo);

    Task<IEnumerable<ShebaInquiryResponseModel>> SearchShebaInquiry(string searchText);

}