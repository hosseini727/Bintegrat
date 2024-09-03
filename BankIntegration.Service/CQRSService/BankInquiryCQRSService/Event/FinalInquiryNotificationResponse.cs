using BankIntegration.Infra.ElasticMapping;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Event;

public class FinalInquiryNotificationResponse : INotification
{
    public FinalInquiry Response { get; }

    public FinalInquiryNotificationResponse(FinalInquiryResponseModel response)
    {
        Response = response;
    }
}