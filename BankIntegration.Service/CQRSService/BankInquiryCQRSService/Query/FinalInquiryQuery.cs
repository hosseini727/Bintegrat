using BankIntegration.Service.Model.BankInquiry;
using MediatR;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query
{
    public record FinalInquiryQuery(string transactionId) : IRequest<FinalInquiryResponseModel>
    {
        public string TransactionId = transactionId;
    }
}
