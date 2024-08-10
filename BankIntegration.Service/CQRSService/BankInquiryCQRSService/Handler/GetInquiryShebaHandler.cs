using AutoMapper;
using BankIntegration.Infra.ThirdApi;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using BankIntegration.Service.MiddleWare.Exception;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Handler;

public class GetInquiryShebaHandler : IRequestHandler<GetInquiryShebaQuery, ShebaInquiryResponseModel>
{
    private readonly IInquiryBankHttp _bankHttp;
    private readonly IMapper _mapper;
    private readonly IAPIkeyService _apIkeyService;

    public GetInquiryShebaHandler(IInquiryBankHttp bankHttp, IMapper mapper, IAPIkeyService apIkeyService)
    {
        _bankHttp = bankHttp;
        _mapper = mapper;
        _apIkeyService = apIkeyService;
    }

    public async Task<ShebaInquiryResponseModel> Handle(GetInquiryShebaQuery request,
        CancellationToken cancellationToken)
    {
        var token = await _apIkeyService.GetShebaInquiryApiKey();
        ShebaInquiryResponseModel response;
        var result = await _bankHttp.GetSebaInquiry(request.AccountNo, token);
        if (!result.IsSuccess)
            throw new BadRequestException($"{result.Message} -- {result.HttpStatus}");
        response = _mapper.Map<FinalResponseInquery, ShebaInquiryResponseModel>(result.Data);
        return response;
    }
}