using ASPBoilerplate.Configurations;
using Microsoft.Extensions.Caching.Distributed;

namespace ASPBoilerplate.Services;
class CacheService
{
    private IDistributedCache _cache;

    public string? GetString(string key)
    {
        var cachedItem = _cache.GetString(key);
        return cachedItem;
    }
    public void CreateString(string key, string value)
    {
        _cache.SetString(
            key, 
            value, 
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = RedisSettings.DefaultExpiry }
        );
    }
}