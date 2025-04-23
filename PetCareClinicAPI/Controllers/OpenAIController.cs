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
    public class OpenAIController : ControllerBase
    {

        private readonly SecretCacheService _secretService;
        private readonly ILogger<OpenAIController> _logger;

        public OpenAIController(SecretCacheService secretService, ILogger<OpenAIController> logger)
        {
            _secretService = secretService;
            _logger = logger;
        }

        [HttpPost(Name = "ask")]
        public async Task<IActionResult> AskQuestion([FromBody] QuestionRequest request)
        {
            try
            {
                var secret = await _secretService.GetSecretAsync();
                var chatClient = new ChatClient("gpt-4.1", secret);

                var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a helpful veterinary assistant giving general medical advice."),
                new UserChatMessage(request.Question)
            };

                var response = await chatClient.CompleteChatAsync(messages);
                string answer = response.Value.Content[0].Text;

                return Ok(answer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accessing chat client.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

    public class QuestionRequest
    {
        public required string Question { get; set; }
    }
}
