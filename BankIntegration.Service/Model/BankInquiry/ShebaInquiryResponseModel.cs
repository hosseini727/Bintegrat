namespace BankIntegration.Service.Model.BankInquiry;

public class ShebaInquiryResponseModel
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

// public class BankInfo
// {
//     public BankInfoName Name { get; set; }
// }
//
// public class BankInfoName
// {
//     public string En { get; set; } = string.Empty;
//     public string Fa { get; set; } = String.Empty;
// }

public class AccountOwner
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
}