using BankIntegration.Infra.ElasticMapping;
using BankIntegration.Infra.MessageBus;
using BankIntegration.Infra.Repository.ElasticRepository.RepositoryInterface;
using BankIntegration.Service.Model.BankInquiry;
using MediatR;

namespace BankIntegration.Service.CQRSService.BankInquiryCQRSService.Event;

public class GetShebaInquiryEventHandler(IElasticGenericRepository<ShebaInquiry> elasticRepo)
    : INotificationHandler<GetShebaInquiryNotificationResponse>
{
    public async Task Handle(GetShebaInquiryNotificationResponse notification, CancellationToken cancellationToken)
    {
        await elasticRepo.SingleDocument(notification.Response);
    }
}