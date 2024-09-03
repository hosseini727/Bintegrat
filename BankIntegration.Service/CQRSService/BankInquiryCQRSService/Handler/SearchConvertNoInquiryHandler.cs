using AutoMapper;
using BankIntegration.Infra.ElasticMapping;
using BankIntegration.Infra.Repository.ElasticRepository.RepositoryInterface;
using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Handler
{
    public class SearchConvertNoInquiryHandler : IRequestHandler<SearchConvertNoInquiryQuery, IEnumerable<ConvertAccountNoResponseModel>>
    {
        private readonly IElasticGenericRepository<ShebaInquiry> _elasticGenericRepository;
        private readonly IMapper _mapper;

        public SearchConvertNoInquiryHandler(IElasticGenericRepository<ShebaInquiry> elasticGenericRepository, IMapper mapper)
        {
            _elasticGenericRepository = elasticGenericRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConvertAccountNoResponseModel>> Handle(SearchConvertNoInquiryQuery request,
            CancellationToken cancellationToken)
        {      
            var result = await _elasticGenericRepository.FullTextSearch(request.SearchText);
            var mappedResult = _mapper.Map<IEnumerable<ShebaInquiry>, IEnumerable<ConvertAccountNoResponseModel>>(result);
            return mappedResult;
        }
    }

}
