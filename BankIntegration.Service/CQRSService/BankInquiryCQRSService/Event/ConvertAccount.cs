using BankIntegration.Infra.ElasticMapping;
using BankIntegration.Infra.Repository.ElasticRepository.RepositoryInterface;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Event;

public class ConvertAccount : INotificationHandler<ConvertAccountNotificationResponse>
{

    private readonly IElasticGenericRepository<ConvertAccountInquiry> _elasticRepo;

    public ConvertAccount(IElasticGenericRepository<ConvertAccountInquiry> elasticRepo)
    {
        _elasticRepo = elasticRepo;
    }
    public async Task Handle(ConvertAccountNotificationResponse notification, CancellationToken cancellationToken)
    {
        await _elasticRepo.SingleDocument(notification.Response);
    }
}