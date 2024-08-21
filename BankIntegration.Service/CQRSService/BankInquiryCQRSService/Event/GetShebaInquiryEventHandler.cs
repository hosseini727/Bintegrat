using BankIntegration.Infra.ElasticMapping;
using BankIntegration.Infra.Repository.ElasticRepository.RepositoryInterface;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Event;

public class GetShebaInquiryEventHandler : INotificationHandler<GetShebaInquiryNotificationResponse>
{

    private readonly IElasticGenericRepository<ShebaInquiry> _elasticRepo;

    public GetShebaInquiryEventHandler(IElasticGenericRepository<ShebaInquiry> elasticRepo)
    {
        _elasticRepo = elasticRepo;
    }
    public async Task Handle(GetShebaInquiryNotificationResponse notification, CancellationToken cancellationToken)
    {
        await _elasticRepo.SingleDocument(notification.Response);
    }
}