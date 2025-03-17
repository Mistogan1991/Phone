using Microsoft.AspNetCore.Mvc;
using Phone.Api.Services;

namespace Phone.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CombinationsController : ControllerBase
    {
        private readonly PhoneCombinationService _service;

        public CombinationsController()
        {
            _service = new PhoneCombinationService();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult GetCombinations([FromBody] PhoneNumberRequest request)
        {
            if (string.IsNullOrEmpty(request.PhoneNumber) || !request.PhoneNumber.All(char.IsDigit))
            {
                return BadRequest(new { error = "Invalid phone number. Only digits 2-9 are allowed." });
            }

            var combinations = _service.GetLetterCombinations(request.PhoneNumber);
            return Ok(new { combinations });
        }
    }

    public class PhoneNumberRequest
    {
        public string PhoneNumber { get; set; }
    }
}
