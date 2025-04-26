using OpenAI.Chat;
using Microsoft.Extensions.Logging;

namespace PetCareClinicAPI.Services
{
    public class OpenAIService
    {
        private readonly SecretCacheService _secretService;
        private readonly ILogger<OpenAIService> _logger;

        public OpenAIService(SecretCacheService secretService, ILogger<OpenAIService> logger)
        {
            _secretService = secretService;
            _logger = logger;
        }

        public async Task<string> ProcessOpenAIRequestAsync(string systemPrompt, string userMessage)
        {
            try
            {
                var secret = await _secretService.GetSecretAsync();
                var chatClient = new ChatClient("gpt-4.1", secret);

                var messages = new List<ChatMessage>
                {
                    new SystemChatMessage(systemPrompt),
                    new UserChatMessage(userMessage)
                };

                var response = await chatClient.CompleteChatAsync(messages);

                if (response.Value.Content == null || response.Value.Content.Count == 0)
                {
                    _logger.LogError("The response content is empty.");
                    throw new InvalidOperationException("The response content is empty.");
                }

                return response.Value.Content[0].Text;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing OpenAI request");
                throw;
            }
        }
    }
} 