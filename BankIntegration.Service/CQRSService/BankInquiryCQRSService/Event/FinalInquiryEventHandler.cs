using BankIntegration.Infra.ElasticMapping;
using BankIntegration.Infra.Repository.ElasticRepository.RepositoryInterface;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Event;

public class FinalInquiryEventHandler : INotificationHandler<FinalInquiryNotificationResponse>
{

    private readonly IElasticGenericRepository<FinalInquiry> _elasticRepo;

    public FinalInquiryEventHandler(IElasticGenericRepository<FinalInquiry> elasticRepo)
    {
        _elasticRepo = elasticRepo;
    }
    public async Task Handle(FinalInquiryNotificationResponse notification, CancellationToken cancellationToken)
    {
        await _elasticRepo.SingleDocument(notification.Response);
    }
}