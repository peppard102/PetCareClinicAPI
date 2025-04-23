using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;

namespace PetCareClinicAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SymptomCheckerController : ControllerBase
    {
        private readonly SecretCacheService _secretService;
        private readonly ILogger<SymptomCheckerController> _logger;

        public SymptomCheckerController(SecretCacheService secretService, ILogger<SymptomCheckerController> logger)
        {
            _secretService = secretService;
            _logger = logger;
        }

        [HttpPost(Name = "checkSymptoms")]
        public async Task<IActionResult> CheckSymptoms([FromBody] SymptomRequest request)
        {
            try
            {
                var secret = await _secretService.GetSecretAsync();

                // Initialize OpenAI client
                var chatClient = new ChatClient("gpt-4.1", secret);

                // Send the question to OpenAI
                var messages = new List<ChatMessage>
                {
                    new SystemChatMessage("You are a helpful veterinary assistant. A pet owner has reported the following symptoms to the veterinarian. Provide a detailed and helpful response, including possible causes and next steps that the veterinarian can take, such as specific tests to run, in order to make an official diagnosis."),
                    new UserChatMessage($"Symptoms: {request.Input}")
                };

                var response = await chatClient.CompleteChatAsync(messages);

                // Extract the answer
                string answer = response.Value.Content[0].Text;

                return Ok(answer );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accessing chat client.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

    public class SymptomRequest
    {
        public required string Input { get; set; }
    }
}
