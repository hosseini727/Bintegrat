using BankIntegration.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace SOS.Domain.Entities;

public class People : IdentityUser<long> , IEntity
{
    #region |Properties|
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    #endregion
    
}