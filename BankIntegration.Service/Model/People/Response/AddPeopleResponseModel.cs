namespace BankIntegration.Service.Model.People.Response;

public record AddPeopleResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}