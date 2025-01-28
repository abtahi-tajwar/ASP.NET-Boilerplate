using ASPBoilerplate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

// using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

class ASPBoilerplateWebApplicationFactory : WebApplicationFactory<ASPBoilerplate.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Remove the existing DbContextOptions
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));

            // Register a new DBContext that will use our test connection string
            string? connString = GetConnectionString();
            services.AddSqlite<AppDbContext>(connString);

            // Add the authentication handler
            // services.AddAuthentication(defaultScheme: "TestScheme")
            //     .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
            //         "TestScheme", options => { });

            // Delete the database (if exists) to ensure we start clean
            AppDbContext dbContext = CreateDbContext(services);
            dbContext.Database.EnsureDeleted();
        });
    }
    private static string GetConnectionString()
    {
        // var configuration = new ConfigurationBuilder()
        //     .AddUserSecrets<MatchMakerWebApplicationFactory>()
        //     .Build();

        // var connString = configuration.GetConnectionString("MatchMaker");
        // return connString;
        return "Data Source=test.db";
    }

    private static AppDbContext CreateDbContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        return dbContext;
    }    
}