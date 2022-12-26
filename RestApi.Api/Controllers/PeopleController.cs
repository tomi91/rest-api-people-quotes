using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi.Core.DTOs;
using RestApi.Core.Services.Person;

namespace RestApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : BaseController
    {
        private readonly IPersonService _personService;

        public PeopleController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ApiResult(await _personService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return ApiResult(await _personService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(PersonDto personDto)
        {
            var result = await _personService.Insert(personDto);
            return InsertApiResult(result, nameof(GetById), result.Data?.Id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, PersonDto personDto)
        {
            return ApiResult(await _personService.Update(id, personDto));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ApiResult(await _personService.Delete(id));
        }


    }
}
