namespace BankIntegration.Service.Model.People.Request;
public record AddPeopleRequestModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }


}