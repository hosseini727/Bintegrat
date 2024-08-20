using BankIntegration.Infra.SharedModel.BankApi;
using Microsoft.Extensions.Options;

namespace BankIntegration.Infra.ThirdApi.Base;

public abstract class BankHttpBase
{
    protected readonly BankSettingModel _bankSetting;
    private readonly IHttpClientFactory _http;

    protected BankHttpBase(IOptions<BankSettingModel> bankSetting, IHttpClientFactory http)
    {
        _bankSetting = bankSetting.Value;
        _http = http;
    }

    protected HttpClient CreateClient()
    {
        return _http.CreateClient(_bankSetting.ClientName);
    }

    protected HttpClient SetHeader(HttpClient client)
    {
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("_token_", _bankSetting.Token);
        client.DefaultRequestHeaders.Add("_token_issuer_", "1");

        return client;
    }

    protected FormUrlEncodedContent SetBodyFormat(Dictionary<string, string> body)
    {
        return new FormUrlEncodedContent(body);
    }

    protected abstract Dictionary<string, string> SetBody(string jsonInput , string apiKey);
    
}