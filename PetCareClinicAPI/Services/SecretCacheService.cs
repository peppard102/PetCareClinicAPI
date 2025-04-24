using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Caching.Memory;

public class SecretCacheService
{
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _config;
    private readonly ILogger<SecretCacheService> _logger;

    private const string CacheKey = "OpenAISecret";
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(1); // optional refresh logic

    public SecretCacheService(IMemoryCache cache, IConfiguration config, ILogger<SecretCacheService> logger)
    {
        _cache = cache;
        _config = config;
        _logger = logger;
    }

    public async Task<string> GetSecretAsync()
    {
        if (_cache.TryGetValue(CacheKey, out object value) && value is string secretValue)
        {
            return secretValue;
        }

        return await LoadSecretAsync();
    }

    private async Task<string> LoadSecretAsync()
    {
        try
        {
            string keyVaultUrl = _config["KeyVault:VaultUri"] ?? throw new InvalidOperationException("KeyVault:VaultUri is not configured.");
            var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
            var secret = await client.GetSecretAsync("OPENAI-API-KEY");

            if (secret.Value?.Value == null)
            {
                throw new InvalidOperationException("The retrieved secret value is null.");
            }

            _cache.Set(CacheKey, secret.Value.Value, _cacheDuration);
            _logger.LogInformation("OpenAI API key loaded into memory cache.");
            return secret.Value.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve OpenAI API key.");
            throw;
        }
    }
}
