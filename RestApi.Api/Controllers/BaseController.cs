using Microsoft.AspNetCore.Mvc;
using RestApi.Core.DTOs.Common;

namespace RestApi.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult ApiResult(IApiResult result)
        {
            if (result.Success)
            {
                if (result.Data == null)
                    return NotFound();

                return Ok(result.Data);
            }

            return BadRequest(result.Messages);
        }

        protected IActionResult InsertApiResult(IApiResult result, string getMethod, object? resultId)
        {
            if (result.Success)
            {
                if (result.Data == null)
                    return BadRequest(result.Messages);

                return CreatedAtAction(getMethod, new { id = resultId }, result.Data);
            }

            return BadRequest(result.Messages);
        }

    }
}
