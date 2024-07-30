namespace BankIntegration.Infra.ThirdApi;

public interface IBankHttp
{
    Task<string> GetResponse();

    Task<string> GetSebaInquiry(string accountNumber);
}