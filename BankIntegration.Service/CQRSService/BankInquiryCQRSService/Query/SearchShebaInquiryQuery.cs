using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;

public class SearchShebaInquiryQuery : IRequest<IEnumerable<ShebaInquiryResponseModel>>
{
    public string SearchText { get; }
    public SearchShebaInquiryQuery(string searchText)
    {
        SearchText = searchText;
    }
}