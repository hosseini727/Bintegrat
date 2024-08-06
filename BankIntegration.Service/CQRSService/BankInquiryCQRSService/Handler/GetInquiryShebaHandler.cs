using AutoMapper;
using BankIntegration.Infra.ThirdApi;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using BankIntegration.Service.MiddleWare.Exception;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Handler;

public class GetInquiryShebaHandler : IRequestHandler<GetInquiryShebaQuery, ShebaInquiryResponseModel>
{
    private readonly IBankHttp _bankHttp;
    private readonly IMapper _mapper;

    public GetInquiryShebaHandler(IBankHttp bankHttp, IMapper mapper)
    {
        _bankHttp = bankHttp;
        _mapper = mapper;
    }

    public async Task<ShebaInquiryResponseModel> Handle(GetInquiryShebaQuery request,
        CancellationToken cancellationToken)
    {
        ShebaInquiryResponseModel response;
        var result = await _bankHttp.GetSebaInquiry(request.AccountNo);
        if (!result.IsSuccess)
            throw new BadRequestException($"{result.Message} -- {result.HttpStatus}");
        response = _mapper.Map<FinalResponseInquery, ShebaInquiryResponseModel>(result.Data);
        return response;
    }
}