namespace BankIntegration.Domain.Entities;

public class NewPasargad_Product : BaseEntity
{
    public NewPasargad_Product()
    {
        this.NewPasargad_ApiProductKey = new HashSet<NewPasargad_ApiProductKey>();
        this.Children  = new HashSet<NewPasargad_Product>();
    }
    #region |Properties|

    public string Name { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public long? ParentProductId { get; set; }

    #endregion

    #region |Navigation|

    public virtual ICollection<NewPasargad_ApiProductKey> NewPasargad_ApiProductKey { get; set; } =
        new HashSet<NewPasargad_ApiProductKey>();

    public virtual NewPasargad_Product Parent { get; set; }
    public virtual ICollection<NewPasargad_Product> Children { get; set; }

    #endregion
}