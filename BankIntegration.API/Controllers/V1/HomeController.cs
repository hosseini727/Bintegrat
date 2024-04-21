using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
namespace BankIntegration.API.Controllers.V1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Home()
    {
        return Ok("GHazi");
    }
}