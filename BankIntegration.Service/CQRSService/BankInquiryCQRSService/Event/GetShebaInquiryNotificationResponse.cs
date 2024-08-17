using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Event;

public class GetShebaInquiryNotificationResponse : INotification
{
    public ShebaInquiryResponseModel Response { get; }

    public GetShebaInquiryNotificationResponse(ShebaInquiryResponseModel response)
    {
        Response = response;
    }
}