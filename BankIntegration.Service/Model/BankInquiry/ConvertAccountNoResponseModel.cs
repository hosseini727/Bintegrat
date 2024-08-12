using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.Model.BankInquiry
{
    public class ConvertAccountNoResponseModel
    {
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string DepositNumber { get; set; } = string.Empty;
        public string DepositIBAN { get; set; } = string.Empty;

    }
}
