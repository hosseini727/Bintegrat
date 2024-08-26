using AutoMapper;
using BankIntegration.Infra.ThirdApi;
using BankIntegration.Infra.ThirdApi.BankModels;
using BankIntegration.Infra.ThirdApi.ConvertAccount;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using BankIntegration.Service.MiddleWare.Exception;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankIntegration.Infra.ThirdApi.ConvertAccount;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Handler
{
    public class FinalInquiryQueryHandler : IRequestHandler<FinalInquiryQuery, FinalInquiryResponseModel>
    {
        private readonly IFinalInquiryBankHttp _bankHttp;
        private readonly IMapper _mapper;
        private readonly IAPIkeyService _apIkeyService;
        public FinalInquiryQueryHandler(IFinalInquiryBankHttp bankHttp, IMapper mapper, IAPIkeyService apIkeyService)
        {
            _bankHttp = bankHttp;
            _mapper = mapper;
            _apIkeyService = apIkeyService;
        }

        public async Task<FinalInquiryResponseModel> Handle(FinalInquiryQuery request,
       CancellationToken cancellationToken)
        {
            var token = await _apIkeyService.GetFinalInquiryApiKey();
            FinalInquiryResponseModel response;            
            var result = await _bankHttp.FinalInquiry(request.TransactionId, token);
            if (!result.IsSuccess)
                throw new BadRequestException($"{result.Message} -- {result.HttpStatus}");
            response = _mapper.Map<FinalResponseInquiry, FinalInquiryResponseModel>(result.Data);
            return response;
        }

    }
}
