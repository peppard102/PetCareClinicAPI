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

        _ = LoadSecretAsync(); // fire and forget on startup
    }

    public async Task<string> GetSecretAsync()
    {
        return _cache.TryGetValue(CacheKey, out string value)
            ? value
            : await LoadSecretAsync();
    }

    private async Task<string> LoadSecretAsync()
    {
        try
        {
            string keyVaultUrl = _config["KeyVault:VaultUri"];
            var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
            var secret = await client.GetSecretAsync("OPENAI-API-KEY");

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
