using System.Text;
using BankIntegration.Infra.SharedModel;
using BankIntegration.Infra.ThirdApi.BankModels;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Newtonsoft.Json;


namespace BankIntegration.Infra.ThirdApi;

public class BankHttp : IBankHttp
{
    private readonly BankSettingModel _bankSetting;
    private readonly IHttpClientFactory _http;

    public BankHttp(IOptions<BankSettingModel> bankSetting, IHttpClientFactory http)
    {
        _http = http;
        _bankSetting = bankSetting.Value;
    }

    public Task<string> GetResponse()
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetSebaInquiry(string accountNumber)
    {
        var token = _bankSetting.Token;
        var body = new ShebaInquiryInputModel
        {
            Iban = accountNumber
        };
        var jsonBody = JsonConvert.SerializeObject(body);

        var data = new Dictionary<string, string>
        {
            { "scProductId", _bankSetting.ShebaInquiryProductCode },
            { "scApiKey", _bankSetting.ShebaInquiryApiKey },
            { "request", jsonBody }
        };
        var content = new FormUrlEncodedContent(data);
        var client = _http.CreateClient("PodApi");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("_token_", _bankSetting.Token);
        client.DefaultRequestHeaders.Add("_token_issuer_", "1");
        var response = await client.PostAsync(_bankSetting.BaseUrl, content);
        
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadAsStringAsync();
            var firstResponseLayer = JsonConvert.DeserializeObject<ShebaInquiryFirstModel>(res);
            if (!firstResponseLayer.HasError)
            {
                var secondResponseLayer = JsonConvert.DeserializeObject<FinalResponseInquery>(firstResponseLayer.Result.result);
            }
        }
        return token;
    }
}