namespace BankIntegration.Infra.SharedModel.BankApi;

public record BankSettingModel
{
    public string Token { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string ShebaInquiryProductCode { get; set; } = string.Empty;
    public string DepositInquiryProductCode { get; set; } = string.Empty;
    
    public string DepositNumber { get; set; } = string.Empty;
    public string ShebaInquiryApiKey { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
}