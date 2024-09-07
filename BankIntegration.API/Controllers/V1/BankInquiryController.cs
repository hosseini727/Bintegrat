using Asp.Versioning;
using BankIntegration.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BankIntegration.API.Controllers.V1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class BankInquiryController : ControllerBase
{
    private readonly IInquiryBankService _inquiryBankService;

    public BankInquiryController(IInquiryBankService inquiryBankService)
    {
        _inquiryBankService = inquiryBankService;
    }

    [HttpGet]
    [Route("Sheba")]
    public async Task<IActionResult> GetShebaInquiry(string accountNo)
    {
        var result = await _inquiryBankService.GetShebaInquiry(accountNo);
        return Ok(result);
    }


    [HttpGet]
    [Route("ConvertAccountNo")]
    public async Task<IActionResult> ConvertAccountNo(string accountNo)
    {
        var result = await _inquiryBankService.ConvertAccountNo(accountNo);
        return Ok(result);
    }


    [HttpGet]
    [Route("FinalInquiry")]
    public async Task<IActionResult> FinalInquiry(string transactionId)
    {
        var result = await _inquiryBankService.FinalInquiry(transactionId);
        return Ok(result);
    }

    [HttpGet]
    [Route("SearchShebaInquiry")]
    public async Task<IActionResult> SearchShebaInquiry(string searchText)
    {
        var result = await _inquiryBankService.SearchShebaInquiry(searchText);
        return result.Any() ? Ok(result) : NoContent();
    }


    [HttpGet]
    [Route("SearchConvertAccountInquiry")]
    public async Task<IActionResult> SearchAccountConvertInquiry(string searchText)
    {
        var result = await _inquiryBankService.SearchConvertAccountNoInquiry(searchText);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet]
    [Route("SearchFinalInquiry")]
    public async Task<IActionResult> SearchFinalInquiry(string searchText)
    {
        var result = await _inquiryBankService.SearchFinalInquiry(searchText);
        return result.Any() ? Ok(result) : NoContent();
    }
}