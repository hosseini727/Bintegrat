using BankIntegration.Service.Model.BankInquiry;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query
{
    public record SearchConvertNoInquiryQuery : IRequest<IEnumerable<ConvertAccountNoResponseModel>>
    {
        public string SearchText { get; }
        public SearchConvertNoInquiryQuery(string searchText)
        {
            SearchText = searchText;
        }
    }

}
