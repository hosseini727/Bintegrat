namespace BankIntegration.Infra.SharedModel.BankApi;

public class ApiResponseModel<T> where T : class
{
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public int HttpStatus { get; set; }
    public T Data { get; set; }
}