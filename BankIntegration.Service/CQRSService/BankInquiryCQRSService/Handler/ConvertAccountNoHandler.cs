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
    public class ConvertAccountNoHandler : IRequestHandler<ConvertAccountNoQuery, ConvertAccountNoResponseModel>
    {
        private readonly IConvertAccountNoBankHttp _bankHttp;
        private readonly IMapper _mapper;
        private readonly IAPIkeyService _apIkeyService;
        //ConvertAccountNo
        public ConvertAccountNoHandler(IConvertAccountNoBankHttp bankHttp, IMapper mapper, IAPIkeyService apIkeyService)
        {
            _bankHttp = bankHttp;
            _mapper = mapper;
            _apIkeyService = apIkeyService;
        }

        public async Task<ConvertAccountNoResponseModel> Handle(ConvertAccountNoQuery request,
       CancellationToken cancellationToken)
        {
            var token = await _apIkeyService.GetDepositInquiryApiKey();
            ConvertAccountNoResponseModel response;            
            var result = await _bankHttp.ConvertAccountNo(request.DepositNo, token);
            if (!result.IsSuccess)
                throw new BadRequestException($"{result.Message} -- {result.HttpStatus}");
            response = _mapper.Map<FinalResponseDepositInquery, ConvertAccountNoResponseModel>(result.Data);
            return response;
        }

    }
}
