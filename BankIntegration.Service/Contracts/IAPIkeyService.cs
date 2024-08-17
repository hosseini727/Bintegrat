namespace BankIntegration.Service.Contracts;

public interface IAPIkeyService
{
    Task<string> GetShebaInquiryApiKey();

    Task<string> GetDepositInquiryApiKey();

    
}