namespace BankIntegration.Service.Model;

public record ProductResponseModel
{
    public long Id { get; set; }
    public string ProductCode { get; set; } = String.Empty;
    public string Name { get; set; } = string.Empty;

}