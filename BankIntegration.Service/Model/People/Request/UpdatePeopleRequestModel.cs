namespace BankIntegration.Service.Model.People.Request;
public record UpdatePeopleRequestModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}