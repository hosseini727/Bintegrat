namespace BankIntegration.Domain.Entities;

public class NewPasargad_ApiProductKey : BaseEntity
{
    #region |Properties|

    public long ProductId { get; set; }
    public string ApiKey { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public DateTime validDate { get; set; }
    public bool IsActive { get; set; }

    #endregion

    #region  |Navigation|
    public virtual NewPasargad_Product? NewPasargad_Product { get; set; }
    #endregion
}