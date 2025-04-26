using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using PetCareClinicAPI.Services;

namespace PetCareClinicAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenAIController : ControllerBase
    {
        private readonly OpenAIService _openAIService;
        private readonly ILogger<OpenAIController> _logger;

        public OpenAIController(OpenAIService openAIService, ILogger<OpenAIController> logger)
        {
            _openAIService = openAIService;
            _logger = logger;
        }

        [HttpPost(Name = "ask")]
        public async Task<IActionResult> AskQuestion([FromBody] QuestionRequest request)
        {
            try
            {
                string systemPrompt = "You are a helpful veterinary assistant giving general medical advice.";
                string answer = await _openAIService.ProcessOpenAIRequestAsync(systemPrompt, request.Question);

                return Ok(answer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accessing chat service.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

    public class QuestionRequest
    {
        public required string Question { get; set; }
    }
}
