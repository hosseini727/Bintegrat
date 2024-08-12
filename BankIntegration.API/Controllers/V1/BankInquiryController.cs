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
    [Route("ShebaInquiry")]
    public async Task<IActionResult> GetShebaInquiry(string accountNo)
    {
        var result = await _inquiryBankService.GetShebaInquiry(accountNo);
        return Ok(result);
    }



    [HttpGet]
    [Route("DepositInquiry")]
    public async Task<IActionResult> GetDepositInquiry(string depositNo)
    {
        var result = await _inquiryBankService.GetDepositInquiry(depositNo);
        return Ok(result);
    }


}