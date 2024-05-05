namespace BankIntegration.Service.Model.People.Response;

public record GetPeopleResponseModel
{
    public string NationalCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;


}