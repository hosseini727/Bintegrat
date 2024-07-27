namespace BankIntegration.Infra.ThirdApi;

public interface IBankHttp
{
    Task<string> GetResponse();
}