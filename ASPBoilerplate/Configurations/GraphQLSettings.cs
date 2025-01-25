using ASPBoilerplate.Services;
using Microsoft.EntityFrameworkCore;

namespace ASPBoilerplate.Configurations
{
    public class GraphQLSettings
    {
        public static void Initialize(WebApplicationBuilder builder)
        {
            // builder.Services.AddPooledDbContextFactory<AppDbContext>(options => options.UseSqlite("GraphqlDb"));

            builder.Services
                .AddGraphQLServer()
                .AddQueryType<Query>();
        }
    }
}