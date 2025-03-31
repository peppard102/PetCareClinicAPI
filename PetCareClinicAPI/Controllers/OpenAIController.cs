using Microsoft.AspNetCore.Mvc;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

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
                var client = new SecretClient(vaultUri: new Uri(keyVaultUrl), credential: new DefaultAzureCredential());


                // Retrieve the secret
                KeyVaultSecret secret = await client.GetSecretAsync(secretName);

                // Return the secret value
                return Ok(new { SecretValue = secret.Value });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving secret: {ex.Message}");
            }
        }
    }
}
