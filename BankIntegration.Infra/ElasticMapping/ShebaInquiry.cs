namespace BankIntegration.Infra.ElasticMapping;

public class ShebaInquiry
{
    public int RsCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string AccountStatus { get; set; } = string.Empty;
    public string AccountComment { get; set; } = string.Empty;
    public string PaymentCode { get; set; } = string.Empty;
    public string PaymentCodeValid { get; set; } = string.Empty;
    public string Iban { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<AccountOwner> IbanAccountOwnerList { get; set; }
    //public BankInfo BankInfo { get; set; }
}

public class AccountOwner
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}