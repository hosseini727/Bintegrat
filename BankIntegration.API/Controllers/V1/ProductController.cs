using Asp.Versioning;
using BankIntegration.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BankIntegration.API.Controllers.V1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    [Route("Get")]
    public IActionResult Get()
    {
        _logger.LogInformation("Api Called");
        return Ok("Api Called");
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAll();
        return result.Any() ? Ok(result) : NoContent();
    }
}