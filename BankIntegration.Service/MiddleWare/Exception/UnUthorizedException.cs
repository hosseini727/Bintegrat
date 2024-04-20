namespace BankIntegration.Service.MiddleWare.Exception;

public class UnUthorizedException : System.Exception
{
    public UnUthorizedException(string message) : base(message)
    {
        
    }
}