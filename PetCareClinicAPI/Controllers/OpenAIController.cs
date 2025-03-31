﻿using Azure.Core;
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

        private readonly IConfiguration _configuration;

        public OpenAIController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet(Name = "GetDiagnosis")]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Get the Key Vault URI from appsettings.json
                string? keyVaultUrl = _configuration["KeyVault:VaultUri"];

                if (string.IsNullOrEmpty(keyVaultUrl))
                {
                    return StatusCode(500, "Key Vault URI is not configured properly.");
                }

                string secretName = "OPENAI-API-KEY";

                // Create a SecretClient using DefaultAzureCredential
                var secretClient = new SecretClient(vaultUri: new Uri(keyVaultUrl), credential: new DefaultAzureCredential());


                // Retrieve the secret
                KeyVaultSecret secret = await secretClient.GetSecretAsync(secretName);

                // Initialize OpenAI client
                var chatClient = new ChatClient("gpt-3.5-turbo", secret.Value);

                // Send the question to OpenAI
                var messages = new List<ChatMessage>
                {
                    new UserChatMessage("how many toes do cats have")
                };

                var response = await chatClient.CompleteChatAsync(messages);

                // Extract the answer
                string answer = response.Value.Content[0].Text;

                return Ok(new { Answer = answer });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving secret: {ex.Message}");
            }
        }
    }
}
