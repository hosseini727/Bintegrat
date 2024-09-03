using BankIntegration.Service.Contracts;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Event;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.Services;

public class InquiryBankService : IInquiryBankService
{
    private readonly IMediator _mediator;

    public InquiryBankService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ShebaInquiryResponseModel> GetShebaInquiry(string accountNo)
    {
        // handleQuery
        var query = new GetInquiryShebaQuery(accountNo);
        var result = await _mediator.Send(query);
        // handleEvent Develop
        var notification = new GetShebaInquiryNotificationResponse(result);
        await _mediator.Publish(notification, default);
        return result;
    }
    public async Task<ConvertAccountNoResponseModel> ConvertAccountNo(string depositNo)
    {   
        var query = new ConvertAccountNoQuery(depositNo);
        var result = await _mediator.Send(query);
        // handleEvent Develop
        var notification = new ConvertAccountNotificationResponse(result);
        await _mediator.Publish(notification, default);
        return result;
    }

    public async Task<FinalInquiryResponseModel> FinalInquiry(string transactionId)
    {
        var query = new FinalInquiryQuery(transactionId);
        var result = await _mediator.Send(query);
        // handleEvent
        var notification = new FinalInquiryNotificationResponse(result);
        await _mediator.Publish(notification);
        return result;
    }
}