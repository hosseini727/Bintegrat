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
        if (string.IsNullOrEmpty(request.AccountNo))
        {
            throw new BadRequestException("account number is Null");
        }
        if (request.AccountNo.Length != 26)
        {
            throw new BadRequestException("length of  account number is not equal 26 character");
        }
        var token = await _apIkeyService.GetShebaInquiryApiKey();
        if (token==null)
            throw new BadRequestException("apikey is Null");
        var result = await _bankHttp.GetSebaInquiry(request.AccountNo, token);
        if (!result.IsSuccess)
            throw new BadRequestException(result.Message);
        if (result.Data == null)
        {
            throw new BadRequestException("data is null from provider");
        }
        result.Data.IsSuccess = result.Data.AccountStatus == "02" ? true : false;
        var response = _mapper.Map<FinalResponseInquery, ShebaInquiryResponseModel>(result.Data);
        return response;
    }
}