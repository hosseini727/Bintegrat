using BankIntegration.Infra.ElasticMapping;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Event;

public class ConvertAccountNotificationResponse : INotification
{
    public ConvertAccountInquiry Response { get; }

    public ConvertAccountNotificationResponse(ConvertAccountNoResponseModel response)
    {
        Response = response;
    }
}