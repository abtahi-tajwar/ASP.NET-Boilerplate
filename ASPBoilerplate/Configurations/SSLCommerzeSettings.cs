namespace ASPBoilerplate.Configurations;

public class SSLCommerzeSettings
{
    public static string? StoreID;
    public static string? StoreSecret;

    public static void Initialize (WebApplicationBuilder builder) {
        StoreID = builder.Configuration["SSLCommerze:StoreID"];
        StoreSecret = builder.Configuration["SSLCommerze:StoreSecret"];
    }
}
