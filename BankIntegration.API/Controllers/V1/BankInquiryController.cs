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
    [Route("SearchConvertNoInquiry")]
    public async Task<IActionResult> SearchConvertAccountNoInquiry(string searchText)
    {
        //test
        var result = await _inquiryBankService.SearchConvertAccountNoInquiry(searchText);
        return Ok(result);
    }


    [HttpGet]
    [Route("SearchFinalInquiry")]
    public async Task<IActionResult> SearchFinalInquiry(string searchText)
    {
        //test
        var result = await _inquiryBankService.SearchFinalInquiry(searchText);
        return Ok(result);
    }
}