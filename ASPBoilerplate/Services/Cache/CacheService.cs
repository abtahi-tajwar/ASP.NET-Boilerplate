using System.Text.Json;
using ASPBoilerplate.Configurations;
using Microsoft.Extensions.Caching.Distributed;

namespace ASPBoilerplate.Services;
public class CacheService
{
    private IDistributedCache _cache;

    public CacheService (IDistributedCache cache)
    {
        _cache = cache;
    }
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
    public T GetOrCreate<T> (string key, Func<T> createItem)
    {
        var cachedItem = _cache.GetString(key);
        if (cachedItem == null)
        {
            var item = createItem();
            _cache.SetString(
                key, 
                JsonSerializer.Serialize(item), 
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = RedisSettings.DefaultExpiry }
            );
            return item;
        } else {
            return JsonSerializer.Deserialize<T>(cachedItem)!;
        }
    }
    public void Create (string key, object val) {
        _cache.SetString(
            key, 
            JsonSerializer.Serialize(val), 
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = RedisSettings.DefaultExpiry }
        );
    }
}