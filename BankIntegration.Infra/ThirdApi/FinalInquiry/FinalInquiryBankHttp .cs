using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Infra.ThirdApi.Base;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace BankIntegration.Infra.ThirdApi.ConvertAccount;

public class FinalInquiryBankHttp : BankHttpBase, IFinalInquiryBankHttp
{
    public FinalInquiryBankHttp(IOptions<BankSettingModel> bankSetting, IHttpClientFactory http) : base(bankSetting, http)
    {
    }
    


    public async Task<ApiResponseModel<FinalResponseInquiry>> FinalInquiry(string transactionId, string apiKey)
    {
        var client = CreateClient();
        SetHeader(client);
        var jsonBody = BuildFinalInquiryBody(transactionId);
        var body = SetBody(jsonBody, apiKey);
        var content = SetBodyFormat(body);
        var response = await client.PostAsync(_bankSetting.BaseUrl, content);
        return await ParseFinalinquiryResponse(response);
    }
    private string BuildFinalInquiryBody(string transactionId)
    {
        var body = new FinalInquiryInputModel
        {
            TransactionId = transactionId
        };
        var jsonBody = JsonConvert.SerializeObject(body);

        return jsonBody;
    }

    private async Task<ApiResponseModel<FinalResponseInquiry>> ParseFinalinquiryResponse(HttpResponseMessage response)
    {
        var result = new ApiResponseModel<FinalResponseInquiry>();
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadAsStringAsync();
            var firstResponseLayer = JsonConvert.DeserializeObject<ShebaInquiryFirstModel>(res);
            if (!firstResponseLayer.HasError)
            {
                var secondResponseLayer =
                    JsonConvert.DeserializeObject<FinalResponseInquiry>(firstResponseLayer.Result.result);
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
            { "scProductId", _bankSetting.FinalInquiryProductCode },
            { "scApiKey", apiKey },
            { "request", jsonInput }
        };
        return data;
    }

}