namespace BankIntegration.Service.MiddleWare.Exception;

public class KeyNotFoundException : System.Exception
{
    public KeyNotFoundException(string message) : base(message)
    {
        
    }
}