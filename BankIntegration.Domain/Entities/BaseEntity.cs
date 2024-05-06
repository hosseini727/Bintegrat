namespace BankIntegration.Domain.Entities;


public interface IEntity
{
}
public class BaseEntity : IEntity
{
    public long Id { get; set; }
    public DateTime?  CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
}