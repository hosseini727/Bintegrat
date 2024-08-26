using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.Model.BankInquiry
{
    public class FinalInquiryResponseModel
    {
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string FinalState { get; set; }
        public string TransactionMessage { get; set; }
        public string FinalMessage { get; set; }

    }
}
