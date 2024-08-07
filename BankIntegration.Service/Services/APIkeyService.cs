using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Service.Contracts;

namespace BankIntegration.Service.Services;

public class APIkeyService : IAPIkeyService
{
    private readonly IUnitOfWork _unitOfWork;

    public APIkeyService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> GetShebaInquiryApiKey()
    {
        var res = await _unitOfWork.ProductRepository.GetAll();
        throw new NotImplementedException();
    }
}