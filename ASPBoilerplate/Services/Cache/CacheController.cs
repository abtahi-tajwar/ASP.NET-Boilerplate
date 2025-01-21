using ASPBoilerplate;
using ASPBoilerplate.Filters;
using ASPBoilerplate.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

[ApiController]
[Route("/dev/utils")]
public class CacheController : ControllerBase {
    private readonly IConnectionMultiplexer _cache;

    public CacheController(IConnectionMultiplexer redis)
    {
        _cache = redis;
    }

    [HttpDelete("clear-cache")]
    [DevAuthorization]
    public async Task<IResult> ClearDatabase()
    {
        var server = _cache.GetServer(_cache.GetEndPoints().First());
        var db = _cache.GetDatabase();

        // Use SCAN to get keys and delete them individually
        var keys = server.Keys();

        foreach (var key in keys)
        {
            await db.KeyDeleteAsync(key);
        }

        return CustomResponse.Ok("All keys in the Redis database have been deleted.");
    }
}