namespace BankIntegration.Infra.SharedModel;

public class BankSettingModel
{
    public string Token { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string ShebaInquiryProductCode { get; set; } = string.Empty;
    public string DepositNumber { get; set; } = string.Empty;
    public string ShebaInquiryApiKey { get; set; } = string.Empty;
}