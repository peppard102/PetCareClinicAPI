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
        private readonly ILogger<VetsController> _logger;
        private readonly IConfiguration _configuration;

        public OpenAIController(ILogger<VetsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost(Name = "ask")]
        public async Task<IActionResult> AskQuestion([FromBody] QuestionRequest request)
        {
            try
            {
                // Get the Key Vault URI from appsettings.json
                string? keyVaultUrl = _configuration["KeyVault:VaultUri"];

                if (string.IsNullOrEmpty(keyVaultUrl))
                {
                    _logger.LogError("Key Vault URI is not configured properly.");
                    return StatusCode(500, "An error occurred while processing your request.");
                }

                // Create a SecretClient using DefaultAzureCredential
                var secretClient = new SecretClient(vaultUri: new Uri(keyVaultUrl), credential: new DefaultAzureCredential());


                // Retrieve the secret
                KeyVaultSecret secret = await secretClient.GetSecretAsync("OPENAI-API-KEY");

                // Initialize OpenAI client
                var chatClient = new ChatClient("gpt-4o-mini", secret.Value);

                // Send the question to OpenAI
                var messages = new List<ChatMessage>
                {
                    new UserChatMessage(request.Question)
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

    public class QuestionRequest
    {
        public required string Question { get; set; }
    }
}
