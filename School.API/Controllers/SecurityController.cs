using Microsoft.AspNetCore.Mvc;
using School.API.Tools;
using School.Contracts.Requests;

namespace School.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {

        private readonly IApiKeyValidation apiKeyValidation;

        public SecurityController(IApiKeyValidation apiKeyValidation)
        {
            this.apiKeyValidation = apiKeyValidation;
        }

        [HttpGet]
        public IActionResult AuthenticateViaQueryParam(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                return BadRequest();
            bool isValid = apiKeyValidation.IsValidApiKey(apiKey);
            if (!isValid)
                return Unauthorized();
            return Ok();
        }

        [HttpPost]
        public IActionResult AuthenticateViaBody([FromBody] RequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ApiKey))
                return BadRequest();
            string apiKey = model.ApiKey;
            bool isValid = apiKeyValidation.IsValidApiKey(apiKey);
            if (!isValid)
                return Unauthorized();
            return Ok();
        }

        [HttpGet("header")]
        public IActionResult AuthenticateViaHeader()
        {
            string? apiKey = Request.Headers[Constants.ApiKeyHeaderName];
            if (string.IsNullOrWhiteSpace(apiKey))
                return BadRequest();
            bool isValid = apiKeyValidation.IsValidApiKey(apiKey);
            if (!isValid)
                return Unauthorized();
            return Ok();
        }

    }
}
