namespace BankIntegration.Service.MiddleWare.Exception;

public class BadRequestException : SystemException
{
    public BadRequestException(string? message) : base(message)
    {
        
    }
}