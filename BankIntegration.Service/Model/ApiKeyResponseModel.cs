namespace BankIntegration.Service.Model;

public record ApiKeyResponseModel
{
    public long ProductId { get; set; }
    public string ApiKey { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public DateTime validDate { get; set; }
    public bool IsActive { get; set; }
}