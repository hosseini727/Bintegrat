namespace BankIntegration.Infra.ThirdApi.BankModels;

public class ShebaInquiryFirstModel
{
    public bool HasError { get; set; }
    public int MessageId { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public int ErrorCode { get; set; }
    public int Count { get; set; }
    public string Ott { get; set; } = string.Empty;
    public Result Result { get; set; }
    public int ScProductId { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class Result
{
    public string result { get; set; }
    public int StatusCode { get; set; }
}

public class FinalResponseInquery
{
    public int RsCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string AccountStatus { get; set; } = string.Empty;
    public string AccountComment { get; set; } = string.Empty;
    public int PaymentCode { get; set; }
    public int PaymentCodeValid { get; set; }
    public string Iban { get; set; } = string.Empty;
    public List<IbanAccountOwner> IbanAccountOwnerList { get; set; }
    public BankInfo BankInfo { get; set; }
}

public class BankInfo
{
    public Name name { get; set; }
}

public class Name
{
    public string en { get; set; }
    public string fa { get; set; }
}


public class IbanAccountOwner
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class CoreLog
{
    public string RefrenceNumber { get; set; } = string.Empty;
    public string ExecutionDate { get; set; } = string.Empty;
    public string TransactionDateTime { get; set; } = string.Empty;
    public string Amount { get; set; } = string.Empty;
    public string BankSwiftCode { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string BranchCode { get; set; } = string.Empty;
    public string SourceIban { get; set; } = string.Empty;
    public string SourceDepositNumber { get; set; } = string.Empty;
    public string DestinationIban { get; set; } = string.Empty;
    public string SourceComment { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string NewFormatState { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public string TransactionNumber { get; set; } = string.Empty;
    public string DestinationBankCode { get; set; } = string.Empty;
    public string DestinationBankName { get; set; } = string.Empty;
}

public class ShebaInquiryInputModel
{
    public string Iban { get; set; } = string.Empty;
}