using Asp.Versioning;
using BankIntegration.Service.Contracts;
using BankIntegration.Service.Model.People.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankIntegration.API.Controllers.V1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] loginPeopleRequestModel model)
        {
            var result = await _peopleService.loginPeople(model);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> CheckedRefreshToken([FromBody] RefreshTokenPeopleRequestModel model)
        {
            var result = await _peopleService.RefreshToken(model);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AddPeopleRequestModel model)
        {
            var addPeople = await _peopleService.AddPeople(model);
            return addPeople != null ? Created(nameof(Add), addPeople) : BadRequest();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetPeople()
        {
            var result = await _peopleService.GetPeople();
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpDelete]
        [Route("{peopleId:int}")]
        public async Task<IActionResult> DeletePeople(int peopleId)
        {
            var result = await _peopleService.DeletePeople(peopleId);
            return result != null ? Ok(result) : NoContent();
        }

        [HttpGet]
        [Route("{peopleId:int}")]
        public async Task<IActionResult> GetPeopleById(int peopleId)
        {
            var result = await _peopleService.GetPeopleById(peopleId);
            return result != null ? Ok(result) : NoContent();
        }
    }
}
