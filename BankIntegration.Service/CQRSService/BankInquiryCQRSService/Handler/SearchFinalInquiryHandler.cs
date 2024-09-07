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
    public class SearchFinalInquiryHandler : IRequestHandler<SearchFinalInquiryQuery, IEnumerable<FinalInquiryResponseModel>>
    {
        private readonly IElasticGenericRepository<FinalInquiry> _elasticGenericRepository;
        private readonly IMapper _mapper;

        public SearchFinalInquiryHandler(IElasticGenericRepository<FinalInquiry> elasticGenericRepository, IMapper mapper)
        {
            _elasticGenericRepository = elasticGenericRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FinalInquiryResponseModel>> Handle(SearchFinalInquiryQuery request,
            CancellationToken cancellationToken)
        {      
            var result = await _elasticGenericRepository.FullTextSearch(request.SearchText);
            var mappedResult = _mapper.Map<IEnumerable<FinalInquiry>, IEnumerable<FinalInquiryResponseModel>>(result);
            return mappedResult;
        }
    }

}
