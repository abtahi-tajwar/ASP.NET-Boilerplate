using StackExchange.Redis;

namespace ASPBoilerplate.Configurations;

class RedisSettings
{
    public static string? Connection;
    public static readonly TimeSpan DefaultExpiry = TimeSpan.FromHours(1);


    public static void Initialize(WebApplicationBuilder builder)
    {
        Connection = builder.Configuration.GetConnectionString("Redis");
        builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Connection!)); // Replace with your Redis server details

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Connection;
            options.InstanceName = "ASPBoilerplate:";
        });

    }
}