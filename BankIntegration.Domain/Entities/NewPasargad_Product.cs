namespace BankIntegration.Domain.Entities;

public class NewPasargad_Product : BaseEntity
{
    #region |Properties|
    //for test
    public string Name { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public long? ParentProductId { get; set; }

    #endregion

    #region |Navigation|

    public virtual ICollection<NewPasargad_ApiProductKey> NewPasargad_ApiProductKey { get; set; } =
        new HashSet<NewPasargad_ApiProductKey>();

    #endregion
}