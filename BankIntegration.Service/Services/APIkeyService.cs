using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using BankIntegration.Infra.SharedModel.BankApi;
using BankIntegration.Service.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BankIntegration.Service.Services;

public class APIkeyService : IAPIkeyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly BankSettingModel _bankSetting;
    private readonly string _shebaCacheKey;
    private readonly string _convertAccountCacheKey;
    private readonly string _finalInquiryCacheKey;

    private readonly IMemoryCache _cache;

    public APIkeyService(IUnitOfWork unitOfWork, IOptions<BankSettingModel> bankSetting, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _bankSetting = bankSetting.Value;
        _shebaCacheKey = "shebaCacheKey";
        _convertAccountCacheKey = "convertAccountCacheKey";
        _finalInquiryCacheKey = "finalInquiryCacheKey";
    }

    public async Task<string> GetShebaInquiryApiKey()
    {
        if (_cache.TryGetValue(_shebaCacheKey, out string apikey))
        {
            return apikey;
        }
        else
        {
            var apikeyValue = await _unitOfWork.ProductApiKeyRepository.GetApikey(_bankSetting.ShebaInquiryProductCode);
            _cache.Set(_shebaCacheKey, apikeyValue, TimeSpan.FromHours(5));
            return apikeyValue;
        }
    }

    public async Task<string> GetDepositInquiryApiKey()
    {
        if (_cache.TryGetValue(_convertAccountCacheKey, out string apikey))
        {
            return apikey;
        }
        else
        {
            var apikeyValue = await _unitOfWork.ProductApiKeyRepository.GetApikey(_bankSetting.DepositInquiryProductCode);
            if (apikeyValue != null)
            {
                _cache.Set(_convertAccountCacheKey, apikeyValue, TimeSpan.FromHours(5));
                return apikeyValue;
            }
            return null;
        }
    }

    //GetFinalInquiryApiKey
    public async Task<string> GetFinalInquiryApiKey()
    {
        if (_cache.TryGetValue(_finalInquiryCacheKey, out string apikey))
        {
            return apikey;
        }
        else
        {
            var apikeyValue = await _unitOfWork.ProductApiKeyRepository.GetApikey(_bankSetting.FinalInquiryProductCode);
            if (apikeyValue != null)
            {
                _cache.Set(_finalInquiryCacheKey, apikeyValue, TimeSpan.FromHours(5));
                return apikeyValue;
            }
            return null;
        }
    }
}