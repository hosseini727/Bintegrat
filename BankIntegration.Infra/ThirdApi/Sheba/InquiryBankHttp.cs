using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Infra.ThirdApi.Base;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace BankIntegration.Infra.ThirdApi.Sheba;

public class InquiryBankHttp : BankHttpBase, IInquiryBankHttp
{
    public InquiryBankHttp(IOptions<BankSettingModel> bankSetting, IHttpClientFactory http) : base(bankSetting, http)
    {
    }

    public async Task<ApiResponseModel<FinalResponseInquery>> GetSebaInquiry(string accountNumber , string apiKey)
    {
        var client = CreateClient();
        SetHeader(client);
        var jsonBody = BuildShebaInquiryBody(accountNumber);
        var body = SetBody(jsonBody , apiKey);
        var content = SetBodyFormat(body);
        var response = await client.PostAsync(_bankSetting.BaseUrl, content);
        return await ParseShebaInquiryResponse(response);
    }

    private string BuildShebaInquiryBody(string accountNumber)
    {
        var body = new ShebaInquiryInputModel
        {
            Iban = accountNumber
        };
        var jsonBody = JsonConvert.SerializeObject(body);

        return jsonBody;
    }

    private async Task<ApiResponseModel<FinalResponseInquery>> ParseShebaInquiryResponse(HttpResponseMessage response)
    {
        var result = new ApiResponseModel<FinalResponseInquery>();
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadAsStringAsync();
            var firstResponseLayer = JsonConvert.DeserializeObject<ShebaInquiryFirstModel>(res);
            if (!firstResponseLayer.HasError)
            {
                var secondResponseLayer =
                    JsonConvert.DeserializeObject<FinalResponseInquery>(firstResponseLayer.Result.result);
                result.IsSuccess = true;
                result.Data = secondResponseLayer;
            }
            else
            {
                result.HttpStatus = (int)response.StatusCode;
                result.IsSuccess = false;
                result.Message = $"{firstResponseLayer.Message} --- با کد خطا {firstResponseLayer.ErrorCode}";
            }
        }
        else
        {
            result.HttpStatus = (int)response.StatusCode;
            result.IsSuccess = false;
            result.Message = response.RequestMessage.Content.ToString();
        }

        return result;
    }

    protected override Dictionary<string, string> SetBody(string jsonInput, string apiKey)
    {
        var data = new Dictionary<string, string>
        {
            { "scProductId", _bankSetting.ShebaInquiryProductCode },
            { "scApiKey", apiKey },
            { "request", jsonInput }
        };
        return data;
    }
}