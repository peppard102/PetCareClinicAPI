using Microsoft.AspNetCore.Mvc;
using PetCareClinicAPI.Services;

namespace PetCareClinicAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SymptomCheckerController : ControllerBase
    {
        private readonly OpenAIService _openAIService;
        private readonly ILogger<SymptomCheckerController> _logger;

        public SymptomCheckerController(OpenAIService openAIService, ILogger<SymptomCheckerController> logger)
        {
            _openAIService = openAIService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CheckSymptoms([FromBody] SymptomRequest request)
        {
            try
            {
                string systemPrompt = "You are a helpful veterinary assistant. A pet owner has reported the following symptoms to the veterinarian. Provide a detailed and helpful response, including possible causes and next steps that the veterinarian can take, such as specific tests to run, in order to make an official diagnosis.";
                string answer = await _openAIService.ProcessOpenAIRequestAsync(systemPrompt, $"Symptoms: {request.Input}");

                return Ok(answer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accessing OpenAI service.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

    public class SymptomRequest
    {
        public required string Input { get; set; }
    }
}
