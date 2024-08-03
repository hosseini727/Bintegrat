using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;

public class GetInquiryShebaQuery(string accountNo) : IRequest<ShebaInquiryResponseModel>
{
    public string AccountNo = accountNo;
}