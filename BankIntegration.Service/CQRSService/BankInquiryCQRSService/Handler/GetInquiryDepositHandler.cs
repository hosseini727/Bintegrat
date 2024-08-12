using AutoMapper;
using BankIntegration.Infra.ThirdApi;
using BankIntegration.Infra.ThirdApi.BankModels;
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

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Handler
{
    public class GetInquiryDepositHandler : IRequestHandler<GetInquiryDepositQuery, DepositInquiryResponseModel>
    {
        private readonly IInquiryDepositBankHttp _bankHttp;
        private readonly IMapper _mapper;
        private readonly IAPIkeyService _apIkeyService;

        public GetInquiryDepositHandler(IInquiryDepositBankHttp bankHttp, IMapper mapper, IAPIkeyService apIkeyService)
        {
            _bankHttp = bankHttp;
            _mapper = mapper;
            _apIkeyService = apIkeyService;
        }

        public async Task<DepositInquiryResponseModel> Handle(GetInquiryDepositQuery request,
       CancellationToken cancellationToken)
        {
            var token = await _apIkeyService.GetDepositInquiryApiKey();
            DepositInquiryResponseModel response;            
            var result = await _bankHttp.GetDepositInquiry(request.DepositNo, token);
            if (!result.IsSuccess)
                throw new BadRequestException($"{result.Message} -- {result.HttpStatus}");
            response = _mapper.Map<FinalResponseDepositInquery, DepositInquiryResponseModel>(result.Data);
            return response;
        }

    }
}
