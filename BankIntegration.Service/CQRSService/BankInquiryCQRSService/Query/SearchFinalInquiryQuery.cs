using BankIntegration.Service.Model.BankInquiry;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query
{
    public class SearchFinalInquiryQuery : IRequest<IEnumerable<FinalInquiryResponseModel>>
    {
        public string SearchText { get; }
        public SearchFinalInquiryQuery(string searchText)
        {
            SearchText = searchText;
        }
    }

}
