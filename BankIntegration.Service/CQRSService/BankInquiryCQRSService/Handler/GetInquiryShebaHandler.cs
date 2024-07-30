using BankIntegration.Infra.ThirdApi;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Handler;

public class GetInquiryShebaHandler : IRequestHandler<GetInquiryShebaQuery,ShebaInquiryResponseModel>
{
    private readonly IBankHttp _bankHttp;

    public GetInquiryShebaHandler(IBankHttp bankHttp)
    {
        _bankHttp = bankHttp;
    }

    public async Task<ShebaInquiryResponseModel> Handle(GetInquiryShebaQuery request, CancellationToken cancellationToken)
    {
        var response = new ShebaInquiryResponseModel();
        var token = await _bankHttp.GetSebaInquiry(request.AccountNo);
        response.Amount = token;
        return response;
    }
}