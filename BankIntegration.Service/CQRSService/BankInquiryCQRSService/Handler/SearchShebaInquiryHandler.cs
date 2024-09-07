using AutoMapper;
using BankIntegration.Infra.ElasticMapping;
using BankIntegration.Infra.Repository.ElasticRepository.RepositoryInterface;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Handler;

public class
    SearchShebaInquiryHandler : IRequestHandler<SearchShebaInquiryQuery, IEnumerable<ShebaInquiryResponseModel>>
{
    private readonly IElasticGenericRepository<ShebaInquiry> _elasticGenericRepository;
    private readonly IMapper _mapper;

    public SearchShebaInquiryHandler(IElasticGenericRepository<ShebaInquiry> elasticGenericRepository, IMapper mapper)
    {
        _elasticGenericRepository = elasticGenericRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShebaInquiryResponseModel>> Handle(SearchShebaInquiryQuery request,
        CancellationToken cancellationToken)
    {
        var t = await _elasticGenericRepository.SearchWithFilter(q =>
            q.Term(t => t.Field(f => f.AccountComment).Value("")));
        
        var result = await _elasticGenericRepository.FullTextSearch(request.SearchText);
        var mappedResult = _mapper.Map<IEnumerable<ShebaInquiry>, IEnumerable<ShebaInquiryResponseModel>>(result);
        return mappedResult;
    }
}