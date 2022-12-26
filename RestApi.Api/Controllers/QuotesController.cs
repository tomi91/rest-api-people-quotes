using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi.Core.DTOs;
using RestApi.Core.Services.Quote;

namespace RestApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : BaseController
    {
        private readonly IQuoteService _quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ApiResult(await _quoteService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return ApiResult(await _quoteService.GetById(id));
        }

        [HttpGet("GetQuoteOfTheDay")]
        public async Task<IActionResult> GetQuoteOfTheDay()
        {
            return ApiResult(await _quoteService.GetQuoteOfTheDay());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(EditQuoteDto quoteDto)
        {
            var result = await _quoteService.Insert(quoteDto);
            return InsertApiResult(result, nameof(GetById), result.Data?.Id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, EditQuoteDto quoteDto)
        {
            return ApiResult(await _quoteService.Update(id, quoteDto));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ApiResult(await _quoteService.Delete(id));
        }


    }
}
