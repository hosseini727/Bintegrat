using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi.BankModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace BankIntegration.Infra.ThirdApi.ConvertAccountNo;

public class ConvertAccountNoBankHttp : BankHttpBase, IConvertAccountNoBankHttp
{
    public ConvertAccountNoBankHttp(IOptions<BankSettingModel> bankSetting, IHttpClientFactory http) : base(bankSetting, http)
    {
    }



    public async Task<ApiResponseModel<FinalResponseDepositInquery>> ConvertAccountNo(string accountNumber, string apiKey)
    {
        var client = CreateClient();
        SetHeader(client);
        var jsonBody = BuildDepositNumberInquiryBody(accountNumber);
        var body = SetBody(jsonBody, apiKey);
        var content = SetBodyFormat(body);
        var response = await client.PostAsync(_bankSetting.BaseUrl, content);
        return await ParseDepositInquiryResponse(response);
    }
    private string BuildDepositNumberInquiryBody(string depositNumber)
    {
        var body = new DepositInquiryInputModel
        {
            DepositNumber = depositNumber
        };
        var jsonBody = JsonConvert.SerializeObject(body);

        return jsonBody;
    }

    private async Task<ApiResponseModel<FinalResponseDepositInquery>> ParseDepositInquiryResponse(HttpResponseMessage response)
    {
        var result = new ApiResponseModel<FinalResponseDepositInquery>();
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadAsStringAsync();
            var firstResponseLayer = JsonConvert.DeserializeObject<ShebaInquiryFirstModel>(res);
            if (!firstResponseLayer.HasError)
            {
                var secondResponseLayer =
                    JsonConvert.DeserializeObject<FinalResponseDepositInquery>(firstResponseLayer.Result.result);
                secondResponseLayer.IsSuccess = true;
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
            { "scProductId", _bankSetting.DepositInquiryProductCode },
            { "scApiKey", apiKey },
            { "request", jsonInput }
        };
        return data;
    }
}