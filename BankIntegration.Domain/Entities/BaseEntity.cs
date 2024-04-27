namespace BankIntegration.Domain.Entities;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime?  CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
}