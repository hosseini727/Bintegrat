namespace BankIntegration.Service.MiddleWare.Exception;

public record validationErrorDetail
{
    public string ErrorMessage { get; set; } = string.Empty;
    public string? StackTrace { get; set; } = string.Empty;
    public List<KeyValuePair<string, string>>? Errors { get; set; }
}